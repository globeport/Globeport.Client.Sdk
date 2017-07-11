using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Async;
using System.Threading;

using MoreLinq;

using Newtonsoft.Json.Schema;

using Globeport.Shared.Library.ApiModel;
using Globeport.Shared.Library.Data;
using Globeport.Shared.Library.ClientModel;
using Globeport.Client.Sdk.Crypto;
using Globeport.Shared.Library.Exceptions;
using Globeport.Shared.Library.Extensions;
using Globeport.Shared.Library.Components;
using Globeport.Client.Sdk.Extensions;
using Globeport.Shared.Library.Utilities;
using Globeport.Shared.Library.Interfaces;  

namespace Globeport.Client.Sdk
{
    public class ApiClient
    {
        public CryptoClient CryptoClient { get; }
        public WebClient WebClient { get; }
        public ApiSession Session { get; private set; }
        public FileStore FileStore { get; private set; }
        public StoreFactory StoreFactory { get; private set; }
        public AtomicBoolean IsProcessingMessages { get; } = new AtomicBoolean(false);

        public ApiClient(WebClient webClient, CryptoClient cryptoClient, StoreFactory storeFactory = null)
        {
            WebClient = webClient;
            CryptoClient = cryptoClient;
            StoreFactory = storeFactory ?? new StoreFactory();
            FileStore = StoreFactory.CreateFileStore(this);
        }

        public Task StartSession(Account account)
        {
            Session = new ApiSession(StoreFactory.CreateCryptoStore(this));

            return Session.Initialize(account);
        }

        //Admin services

        public Task<PingResponse> Ping(CancellationToken token = default(CancellationToken))
        {
            return WebClient.Put<PingResponse>(new Ping(), null, token);
        }

        public Task<SuspendResponse> Suspend(CancellationToken token = default(CancellationToken))
        {
            return WebClient.Put<SuspendResponse>(new Suspend(), Session, token);
        }

        public Task<ResumeResponse> Resume(CancellationToken token = default(CancellationToken))
        {
            return WebClient.Put<ResumeResponse>(new Resume(), Session, token);
        }

        // Auth services

        public async Task<AuthenticateResponse> SignUp(string username, string password, string hardwareId, string platform, CancellationToken token = default(CancellationToken))
        {
            var passwordSalt = CryptoClient.GeneratePasswordSalt();
            var passwordKey = CryptoClient.GeneratePasswordKey(password, passwordSalt);
            var verifier = CryptoClient.GenerateVerifier(username, passwordKey, passwordSalt);
            var credentials = CryptoClient.GenerateClientCredentials(username, passwordKey, passwordSalt);
            var identityKeyPair = CryptoClient.GenerateKeyPair();
            var keys = GenerateRegistrationKeys(passwordKey, identityKeyPair);
            var color = SystemColors.Instance.GetRandomPrimaryColor();
            var avatarData = Entity.GetAvatarData(username, color).ToBytes();
            var signature = CryptoClient.Sign(identityKeyPair.PrivateKey, avatarData);

            var request = new SignUp(Globals.CurrentVersion, username, color, signature, verifier, passwordSalt, keys);

            var response = await WebClient.Post<SignUpResponse>(request, null, token).ConfigureAwait(false);

            var evidence = CryptoClient.GenerateClientEvidence(response.Credentials);

            return await Authenticate(response.SessionId, credentials, evidence, passwordKey, hardwareId, platform, token).ConfigureAwait(false);
        }

        public async Task<AuthenticateResponse> LogIn(string username, string password, string hardwareId, string platform, CancellationToken token = default(CancellationToken))
        {
            var loginRequest = new LogIn(Globals.CurrentVersion, username);

            var loginResponse = await WebClient.Post<LogInResponse>(loginRequest, null, token).ConfigureAwait(false);

            var passwordKey = CryptoClient.GeneratePasswordKey(password, loginResponse.Salt);

            var credentials = CryptoClient.GenerateClientCredentials(username, passwordKey, loginResponse.Salt);

            var evidence = CryptoClient.GenerateClientEvidence(loginResponse.Credentials);

            return await Authenticate(loginResponse.SessionId, credentials, evidence, passwordKey, hardwareId, platform, token).ConfigureAwait(false);
        }

        async Task<AuthenticateResponse> Authenticate(string sessionId, byte[] credentials, byte[] evidence, byte[] passwordKey, string hardwareId, string platform, CancellationToken token = default(CancellationToken))
        {
            var session = new ApiSession(StoreFactory.CreateCryptoStore(this)) { SessionId = sessionId };

            var request = new Authenticate(credentials, evidence, hardwareId, platform, Globals.Culture);

            var response = await WebClient.Post<AuthenticateResponse>(request, session, token).ConfigureAwait(false);

            var sessionKey = CryptoClient.VerifyServerEvidence(response.Evidence);

            if (sessionKey == null) throw new LogInException();

            response.Account.SessionKey = sessionKey;
            response.Account.IsAuthenticated = true;

            var keys = response.Keys.Prepend(new Key(response.Account.Id, KeyType.PasswordKey, passwordKey));

            Session = session;

            Session.Initialize(response.Account, keys);

            DecryptKeys(keys);

            return response;
        }

        public async Task<ChangePasswordResponse> ChangePassword(string currentPassword, string newPassword, CancellationToken token = default(CancellationToken))
        {
            var passwordKey = CryptoClient.GeneratePasswordKey(currentPassword, Session.Salt);

            var challengeRequest = new Challenge();

            var challengeResponse = await WebClient.Post<ChallengeResponse>(challengeRequest, Session, token).ConfigureAwait(false);

            var salt = CryptoClient.GeneratePasswordSalt();
            var credentials = CryptoClient.GenerateClientCredentials(Session.Username, passwordKey, Session.Salt);
            var evidence = CryptoClient.GenerateClientEvidence(challengeResponse.Credentials);
            passwordKey = CryptoClient.GeneratePasswordKey(newPassword, salt);
            var verifier = CryptoClient.GenerateVerifier(Session.Username, passwordKey, salt);
            var masterKey = CryptoClient.Encrypt(Session.KeyStore.MasterKey.Value, passwordKey);

            var request = new ChangePassword(Globals.CurrentVersion, credentials, evidence, verifier, salt, masterKey);

            var response = await WebClient.Put<ChangePasswordResponse>(request, Session, token).ConfigureAwait(false);

            var sessionKey = CryptoClient.VerifyServerEvidence(response.Evidence);

            response.Account.SessionKey = sessionKey;

            Session.Salt = salt;
            Session.SessionKey = sessionKey;

            return response;
        }

        public Task<PutPushUriResponse> PutPushUri(string pushUri, CancellationToken token = default(CancellationToken))
        {
            return PutPushUri(Session, pushUri, token);
        }

        public Task<PutPushUriResponse> PutPushUri(ISession session, string pushUri, CancellationToken token = default(CancellationToken))
        {
            return WebClient.Put<PutPushUriResponse>(new PutPushUri(pushUri), session, token);
        }

        public Task<LogOutResponse> LogOut(string sessionId = null, CancellationToken token = default(CancellationToken))
        {
            return LogOut(Session, sessionId ?? Session.SessionId, token);
        }

        public async Task<LogOutResponse> LogOut(ISession session, string sessionId, CancellationToken token = default(CancellationToken))
        {
            LogOutResponse response;

            if (session.IsAuthenticated)
            {
                var request = new LogOut(sessionId);

                try
                {
                    response = await WebClient.Post<LogOutResponse>(request, session, token).ConfigureAwait(false);
                }
                catch (UnauthorisedException)
                {
                    response = new LogOutResponse();
                }
            }
            else
            {
                response = new LogOutResponse();
            }

            if (sessionId == Session?.SessionId)
            {
                Session = null;
            }

            return response;
        }

        public Task<GetAccountResponse> GetAccount(CancellationToken token = default(CancellationToken))
        {
            return GetAccount(Session, token);
        }

        public Task<GetAccountResponse> GetAccount(ISession session, CancellationToken token = default(CancellationToken))
        {
            var request = new GetAccount();

            return WebClient.Get<GetAccountResponse>(request, session, token);
        }

        public Task<GetUserResponse> GetUser(string username, CancellationToken token = default(CancellationToken))
        {
            var request = new GetUser(username);

            return WebClient.Get<GetUserResponse>(request, null, token);
        }

        public async Task<GetStatusResponse> GetStatus(CancellationToken token = default(CancellationToken))
        {
            var request = new GetStatus();

            var response = await WebClient.Get<GetStatusResponse>(request, Session, token).ConfigureAwait(false);

            return response;
        }

        public Task<GetKeyBundleResponse> GetKeyBundle(string accountId, CancellationToken token = default(CancellationToken))
        {
            var request = new GetKeyBundle(accountId);

            return WebClient.Get<GetKeyBundleResponse>(request, Session, token);
        }

        public Task<GetSessionsResponse> GetSessions(CancellationToken token = default(CancellationToken))
        {
            var request = new GetSessions();

            return WebClient.Get<GetSessionsResponse>(request, Session, token);
        }

        // Activity services

        public Task<GetActivitiesResponse> GetActivities(DataCursor cursor, bool dependencies, CancellationToken token = default(CancellationToken))
        {
            var request = new GetActivities(cursor, dependencies);

            return WebClient.Get<GetActivitiesResponse>(request, Session, token);
        }

        // Analytics services

        public Task<LogEventResponse> LogEvent(string appName, string eventType, string message, string data, CancellationToken token = default(CancellationToken))
        {
            var request = new LogEvent(appName, eventType, message, data);

            return WebClient.Post<LogEventResponse>(request, Session, token);
        }

        // Contact services

        public Task<PostContactResponse> PostContact(string contactId, string avatarId, IEnumerable<string> portals, CancellationToken token = default(CancellationToken))
        {
            var request = new PostContact(contactId, avatarId, portals);

            return WebClient.Post<PostContactResponse>(request, Session, token);
        }

        public Task<PutContactResponse> PutContact(string contactId, string avatarId, IEnumerable<string> addPortals, IEnumerable<string> removePortals, CancellationToken token = default(CancellationToken))
        {
            var request = new PutContact(contactId, avatarId, addPortals, removePortals);

            return WebClient.Put<PutContactResponse>(request, Session, token);
        }

        public Task<DeleteContactResponse> DeleteContact(string contactId, CancellationToken token = default(CancellationToken))
        {
            var request = new DeleteContact(contactId);

            return WebClient.Delete<DeleteContactResponse>(request, Session, token);
        }

        public Task<GetContactsResponse> GetContactById(string contactId, CancellationToken token = default(CancellationToken))
        {
            var request = new GetContacts(new[] { contactId });

            return WebClient.Get<GetContactsResponse>(request, Session, token);
        }

        public Task<GetContactsResponse> GetContactByUsername(string username, CancellationToken token = default(CancellationToken))
        {
            var request = new GetContacts(username);

            return WebClient.Get<GetContactsResponse>(request, Session, token);
        }

        public async Task<GetContactsResponse> GetContacts(IEnumerable<string> contactIds, CancellationToken token = default(CancellationToken))
        {
            if (!contactIds.Any()) return new GetContactsResponse();

            var tasks = new List<Task<GetContactsResponse>>();

            foreach (var batch in contactIds.Batch(Globals.MaxGetCount))
            {
                var request = new GetContacts(batch);

                var task = WebClient.Get<GetContactsResponse>(request, Session, token);

                tasks.Add(task);
            }

            var responses = await Task.WhenAll(tasks).ConfigureAwait(false);

            return new GetContactsResponse(responses.SelectMany(i => i.Contacts));
        }

        public Task<GetContactsResponse> GetContacts(DataCursor cursor, string portalId = null, string mode = null, CancellationToken token = default(CancellationToken))
        {
            var request = new GetContacts(cursor, portalId, mode);

            return WebClient.Get<GetContactsResponse>(request, Session, token);
        }

        public Task<GetAvatarsResponse> GetAvatar(string avatarId, CancellationToken token = default(CancellationToken))
        {
            return GetAvatars(new[] { avatarId });
        }

        public async Task<GetAvatarsResponse> GetAvatars(IEnumerable<string> avatarIds, CancellationToken token = default(CancellationToken))
        {
            if (!avatarIds.Any()) return new GetAvatarsResponse();

            var tasks = new List<Task<GetAvatarsResponse>>();

            foreach (var batch in avatarIds.Batch(Globals.MaxGetCount))
            {
                var request = new GetAvatars(batch);

                var task = WebClient.Get<GetAvatarsResponse>(request, Session, token);

                tasks.Add(task);
            }

            var responses = await Task.WhenAll(tasks).ConfigureAwait(false);

            return new GetAvatarsResponse(responses.SelectMany(i => i.Avatars));
        }

        public Task<GetAvatarsResponse> GetAvatars(DataCursor cursor, string search, string accountId, string portalId, CancellationToken token = default(CancellationToken))
        {
            var request = new GetAvatars(cursor, search, accountId, portalId);

            return WebClient.Get<GetAvatarsResponse>(request, Session, token);
        }

        public Task<GetEntitiesResponse> GetEntity(string entityId, bool dependencies = false, CancellationToken token = default(CancellationToken))
        {
            return GetEntities(new[] { entityId }, dependencies, token);
        }

        public async Task<GetEntitiesResponse> GetEntities(IEnumerable<string> entityIds, bool dependencies, CancellationToken token = default(CancellationToken))
        {
            if (!entityIds.Any()) return new GetEntitiesResponse();

            var tasks = entityIds.Batch(Globals.MaxGetCount).Select(batch =>
            {
                var request = new GetEntities(batch, dependencies);

                return WebClient.Get<GetEntitiesResponse>(request, Session, token);
            });

            var responses = await Task.WhenAll(tasks).ConfigureAwait(false);

            if (dependencies)
            {
                return new GetEntitiesResponse(responses.SelectMany(i => i.Entities), responses.SelectMany(i => i.Classes).Distinct(i => i.Key), responses.SelectMany(i => i.Models).Distinct(i => i.Key), responses.SelectMany(i => i.Contacts).Distinct(i => i.Key));
            }
            else
            {
                return new GetEntitiesResponse(responses.SelectMany(i => i.Entities));
            }
        }

        public Task<GetEntitiesResponse> GetEntities(DataCursor cursor, string portalId, string modelId, bool dependencies, CancellationToken token = default(CancellationToken))
        {
            var request = new GetEntities(cursor, portalId, modelId, dependencies);

            return WebClient.Get<GetEntitiesResponse>(request, Session, token);
        }

        public async Task<PostEntityResponse> PostEntity(string modelId, DataObject data, List<MediaUpload> uploads, IEnumerable<string> portals, CancellationToken token = default(CancellationToken))
        {
            var schema = await FileStore.GetSchema(modelId).ConfigureAwait(false);
            data = new DataPreProcessor(data, JSchema.Parse(schema)).Process();
            var signature = CryptoClient.Sign(Session.KeyStore.PrivateIdentityKey.Value, data.Serialize().ToBytes());
            var isPublic = portals?.Contains(Session.Portals[SystemPortals.Public]) == true;
            var messages = new List<SignalMessage>();
            var keyId = CryptoClient.GenerateId();
            string packetId = null;
            var cipher = new DataCipher(CryptoClient, Session.KeyStore.PrivateIdentityKey.Value, data, schema, uploads).Process();
            var keys = uploads.Where(i => i.KeyId != null).ToDictionary(i => i.KeyId, i => i.Key);
            keys.Add(keyId, CryptoClient.GenerateSecretKey());
            var uploadKeys = new Dictionary<string, byte[]>();

            if (!isPublic)
            {
                packetId = CryptoClient.GenerateId();
                var packetKey = CryptoClient.GenerateSecretKey();
                var packetData = CryptoClient.Encrypt(cipher.PrivateData.Serialize().ToBytes(), packetKey);
                var packet = new Packet(packetId, Session.Portals[SystemPortals.Profile], Session.AccountId, packetData);

                uploadKeys.Add(packetId, packetKey);
                keys.Add(packetId, CryptoClient.GenerateSecretKey());

                var payload = new EntityPayload(packetId, packetData, keys).Serialize().ToBytes();
                var packetMessages = await GenerateMessages(portals, SignalContentType.Entity, payload).ConfigureAwait(false);

                messages.AddRange(packetMessages);
                data = cipher.PublicData;

                Session.PacketStore.Add(packet);
                Session.KeyStore.Add(keys.Select(i => new Key($"{Session.AccountId}.{i.Key}", KeyType.SecretKey, i.Value)));
            }
            else
            {
                uploadKeys.AddRange(keys);
            }

            var request = new PostEntity(modelId, data, signature, keyId, packetId, messages.Select(i => i.GetUpload()), uploads.Where(i => i.IsValid), uploadKeys, portals);

            try
            {
                await Session.CryptoStore.SaveInserts(token).ConfigureAwait(false);

                var response = await WebClient.Post<PostEntityResponse>(request, Session, token).ConfigureAwait(false);

                Session.CryptoStore.SaveDeletes(token).Run();

                return response;
            }
            catch (MismatchedEndpointsException e)
            {
                var endpoints = e.GetEndpoints();

                foreach (var extraEndpoint in endpoints.ExtraEndpoints)
                {
                    messages.RemoveAll(i => i.Address == extraEndpoint.GetAddress());
                    Session.SessionStore.Delete(extraEndpoint.AccountId, extraEndpoint.DeviceId);
                }

                var senderKeyMessages = await GenerateSenderKeyDistributionMessages(endpoints.MissingEndpoints).ConfigureAwait(false);
                messages.AddRange(senderKeyMessages);

                request = new PostEntity(modelId, data, signature, keyId, packetId, messages.Select(i => i.GetUpload()), uploads, uploadKeys, portals);

                await Session.CryptoStore.SaveInserts(token).ConfigureAwait(false);

                var response = await WebClient.Post<PostEntityResponse>(request, Session, token).ConfigureAwait(false);

                Session.CryptoStore.SaveDeletes(token).Run();

                return response;
            }
            finally
            {
                Session.CryptoStore.Reset();
            }
        }

        public async Task<PutEntityResponse> PutEntity(Entity entity, List<MediaUpload> uploads, IEnumerable<string> portals, IEnumerable<string> addPortals, IEnumerable<string> removePortals, CancellationToken token = default(CancellationToken))
        {
            var schema = await FileStore.GetSchema(entity.ModelId).ConfigureAwait(false);
            var data = new DataPreProcessor(entity.Data, JSchema.Parse(schema)).Process();
            var signature = CryptoClient.Sign(Session.KeyStore.PrivateIdentityKey.Value, data.Serialize().ToBytes());
            var isPublic = (!entity.IsPublic && addPortals?.Contains(Session.Portals[SystemPortals.Public]) == true) || (entity.IsPublic && removePortals?.Contains(Session.Portals[SystemPortals.Public]) == false);
            var messages = new List<SignalMessage>();
            string packetId = null;
            byte[] payload = null;
            var cipher = new DataCipher(CryptoClient, Session.KeyStore.PrivateIdentityKey.Value, data, schema, uploads).Process();
            var keys = uploads.Where(i => i.KeyId != null).ToDictionary(i => i.KeyId, i => i.Key);
            var uploadKeys = new Dictionary<string, byte[]>();

            if (!isPublic)
            {
                packetId = CryptoClient.GenerateId();
                var packetKey = CryptoClient.GenerateSecretKey();
                var packetData = CryptoClient.Encrypt(cipher.PrivateData.Serialize().ToBytes(), packetKey);
                var packet = new Packet(packetId, Session.Portals[SystemPortals.Profile], Session.AccountId, packetData);

                uploadKeys.Add(packetId, packetKey);
                keys.Add(packetId, CryptoClient.GenerateSecretKey());
                keys.AddRange(entity.Media.Where(i => i.KeyId != null && cipher.Media.Contains(i.Id)).DistinctBy(i => i.KeyId).ToDictionary(i => i.KeyId, i => i.Key));


                payload = new EntityPayload(packetId, packetData, keys).Serialize().ToBytes();
                var packetMessages = await GenerateMessages(portals, SignalContentType.Entity, payload).ConfigureAwait(false);

                messages.AddRange(packetMessages);
                data = cipher.PublicData;

                Session.PacketStore.Add(packet);
                Session.KeyStore.Add(keys.Select(i => new Key($"{Session.AccountId}.{i.Key}", KeyType.SecretKey, i.Value)));
            }
            else
            {
                uploadKeys.AddRange(keys);
                if (entity.Key != null)
                {
                    uploadKeys.Add(entity.KeyId, entity.Key);
                }
                if (!entity.IsPublic)
                {
                    uploadKeys.AddRange(entity.Media.Where(i => cipher.Media.Contains(i.Id)).DistinctBy(i => i.KeyId).ToDictionary(i => i.KeyId, i => i.Key));
                }
            }

            var request = new PutEntity(entity.Id, data, signature, packetId, messages.Select(i=>i.GetUpload()), uploads.Where(i=>i.IsValid), uploadKeys, addPortals, removePortals);

            try
            {
                await Session.CryptoStore.SaveInserts(token).ConfigureAwait(false);

                var response = await WebClient.Put<PutEntityResponse>(request, Session, token).ConfigureAwait(false);

                Session.CryptoStore.SaveDeletes(token).Run();

                return response;
            }
            catch (MismatchedEndpointsException e)
            {
                var endpoints = e.GetEndpoints();

                foreach (var extraEndpoint in endpoints.ExtraEndpoints)
                {
                    messages.RemoveAll(i => i.Address == extraEndpoint.GetAddress());
                    Session.SessionStore.Delete(extraEndpoint.AccountId, extraEndpoint.DeviceId);
                }

                var senderKeyMessages = await GenerateSenderKeyDistributionMessages(endpoints.MissingEndpoints).ConfigureAwait(false);
                messages.AddRange(senderKeyMessages);
                var packetMessages = await GenerateMessages(endpoints.MissingPortals.Select(i=>i.Id), SignalContentType.Entity, payload).ConfigureAwait(false);
                messages.AddRange(packetMessages);

                request = new PutEntity(entity.Id, data, signature, packetId, messages.Select(i => i.GetUpload()), uploads, uploadKeys, addPortals, removePortals);

                await Session.CryptoStore.SaveInserts(token).ConfigureAwait(false);

                var response = await WebClient.Put<PutEntityResponse>(request, Session, token).ConfigureAwait(false);

                Session.CryptoStore.SaveDeletes(token).Run();

                response.MissingPortals = endpoints.MissingPortals;
                response.ExtraPortals = endpoints.ExtraPortals;

                return response;
            }
            finally
            {
                Session.CryptoStore.Reset();
            }
        }

        public Task<DeleteEntityResponse> DeleteEntity(string entityId, CancellationToken token = default(CancellationToken))
        {
            var request = new DeleteEntity(entityId);

            return WebClient.Delete<DeleteEntityResponse>(request, Session, token);
        }

        public Task<HideEntityResponse> HideEntity(string entityId, CancellationToken token = default(CancellationToken))
        {
            var request = new HideEntity(entityId);

            return WebClient.Post<HideEntityResponse>(request, Session, token);
        }

        public Task<GetInteractionsResponse> GetInteraction(Entity entity, string interactionId, bool dependencies, CancellationToken token = default(CancellationToken))
        {
            return GetInteractions(entity, new[] { interactionId }, dependencies, token);
        }

        public async Task<GetInteractionsResponse> GetInteractions(Entity entity, IEnumerable<string> interactionIds, bool dependencies, CancellationToken token = default(CancellationToken))
        {
            if (!interactionIds.Any()) return new GetInteractionsResponse();

            var tasks = interactionIds.Batch(Globals.MaxGetCount).Select(batch =>
            {
                var request = new GetInteractions(batch, dependencies);

                return WebClient.Get<GetInteractionsResponse>(request, Session, token);
            });

            var responses = await Task.WhenAll(tasks).ConfigureAwait(false);

            var interactions = responses.SelectMany(i => i.Interactions);

            foreach (var interaction in interactions)
            {
                if (!interaction.IsPublic && entity.Key != null)
                {
                    interaction.Data = CryptoClient.Decrypt(interaction.Data, entity.Key);
                }
                interaction.Properties = interaction.Data.FromBytes().Deserialize<DataObject>();
            }

            return new GetInteractionsResponse(interactions);
        }

        public async Task<GetInteractionsResponse> GetInteractions(Entity entity, string accountId, string type, string filter, DataCursor cursor, bool dependencies, CancellationToken token = default(CancellationToken))
        {
            var request = new GetInteractions(entity.Id, accountId, type, filter, cursor, dependencies);

            var response = await WebClient.Get<GetInteractionsResponse>(request, Session, token).ConfigureAwait(false);

            foreach (var interaction in response.Interactions)
            {
                if (!interaction.IsPublic && entity.Key != null)
                {
                    interaction.Data = CryptoClient.Decrypt(interaction.Data, entity.Key);
                }
                interaction.Properties = interaction.Data.FromBytes().Deserialize<DataObject>();
            }

            return response;
        }

        public async Task<PostInteractionResponse> PostInteraction(Entity entity, string type, string tag, DataObject properties, bool encrypt, CancellationToken token = default(CancellationToken))
        {
            var isPublic = !encrypt || entity.IsPublic;

            if (!isPublic && entity.Key == null)
            {
                //disable interactions for legacy items with no key  
                throw new NotSupportedException();
            }

            properties.RemoveNulls();

            var data = properties.Serialize().ToBytes();

            var signature = CryptoClient.Sign(Session.KeyStore.PrivateIdentityKey.Value, data);

            if (!isPublic)
            {
                data = CryptoClient.Encrypt(data, entity.Key);
            }

            try
            {
                await Session.CryptoStore.SaveInserts(token).ConfigureAwait(false);

                var request = new PostInteraction(entity.Id, type, tag, data, signature, isPublic);

                return await WebClient.Post<PostInteractionResponse>(request, Session, token).ConfigureAwait(false);
            }
            finally
            {
                Session.CryptoStore.Reset();
            }
        }

        public async Task<PutInteractionResponse> PutInteraction(Entity entity, string interactionId, string tag, DataObject properties, bool encrypt, CancellationToken token = default(CancellationToken))
        {
            var isPublic = !encrypt || entity.IsPublic;

            if (!isPublic && entity.Key == null)
            {
                //disable interactions for legacy items with no key  
                throw new NotSupportedException();
            }

            properties.RemoveNulls();

            var data = properties.Serialize().ToBytes();

            var signature = CryptoClient.Sign(Session.KeyStore.PrivateIdentityKey.Value, data);

            if (!isPublic)
            {
                data = CryptoClient.Encrypt(data, entity.Key);
            }

            try
            {
                await Session.CryptoStore.SaveInserts(token).ConfigureAwait(false);

                var request = new PutInteraction(interactionId, tag, data, signature, isPublic);

                return await WebClient.Put<PutInteractionResponse>(request, Session, token).ConfigureAwait(false);
            }
            finally
            {
                Session.CryptoStore.Reset();
            }
        }

        public Task<DeleteInteractionResponse> DeleteInteraction(string interactionId, CancellationToken token = default(CancellationToken))
        {
            var request = new DeleteInteraction(interactionId);

            return WebClient.Delete<DeleteInteractionResponse>(request, Session, token);
        }

        public async Task ProcessMessages(CancellationToken token = default(CancellationToken))
        {
            if (!IsProcessingMessages.CompareAndSet(false, true)) return;
            try
            {
                var isFinished = false;

                while (!isFinished)
                {
                    var request = new GetMessages();

                    var response = await WebClient.Get<GetMessagesResponse>(request, Session, token).ConfigureAwait(false);

                    if (response.Messages.Count > 0)
                    {
                        await ProcessMessages(response.Messages, token).ConfigureAwait(false);
                        await AckMessages(response.Messages.Max(i => i.Id), token).ConfigureAwait(false);
                    }

                    isFinished = response.IsQueueEmpty;
                }
            }
            catch { }
            finally
            {
                IsProcessingMessages.SetValue(false);
            }
        }

        Task AckMessages(string lastMessageId, CancellationToken token = default(CancellationToken))
        {
            var request = new AckMessages(lastMessageId);

            return WebClient.Put<AckMessagesResponse>(request, Session, token);
        }

        public Task<GetKeysResponse> GetKey(string keyId, CancellationToken token = default(CancellationToken))
        {
            return GetKeys(new[] { keyId });
        }

        public async Task<GetKeysResponse> GetKeys(IEnumerable<string> keyIds, CancellationToken token = default(CancellationToken))
        {
            if (!keyIds.Any()) return new GetKeysResponse();

            var tasks = new List<Task<GetKeysResponse>>();

            foreach (var batch in keyIds.Batch(Globals.MaxGetCount/5))
            {
                var request = new GetKeys(batch);

                var task = WebClient.Get<GetKeysResponse>(request, Session, token);

                tasks.Add(task);
            }

            var responses = await Task.WhenAll(tasks).ConfigureAwait(false);

            var keys = responses.SelectMany(i => i.Keys);

            DecryptKeys(keys);

            return new GetKeysResponse(keys);
        }

        public Task<PostKeysResponse> PostKeys(IEnumerable<Key> keys, CancellationToken token = default(CancellationToken))
        {
            if (!keys.Any()) return Task.FromResult(new PostKeysResponse());

            keys = keys.Select(i => (Key)i.Clone()).ToList();

            foreach (var key in keys)
            {
                key.Value = CryptoClient.Encrypt(key.Value, Session.KeyStore.MasterKey.Value);
            }

            var request = new PostKeys(keys.Select(i => i.GetUpload()));

            return WebClient.Post<PostKeysResponse>(request, Session, token);
        }

        public Task DeleteKeys(IEnumerable<string> keyIds, CancellationToken token = default(CancellationToken))
        {
            if (!keyIds.Any()) return Tasks.Complete;

            var tasks = new List<Task<DeleteKeysResponse>>();

            foreach (var batch in keyIds.Batch(Globals.MaxDeleteCount))
            {
                var request = new DeleteKeys(batch);

                var task = WebClient.Delete<DeleteKeysResponse>(request, Session, token);

                tasks.Add(task);
            }

            return Task.WhenAll(tasks);
        }

        public Task<GetPacketsResponse> GetPacket(string packetId, CancellationToken token = default(CancellationToken))
        {
            return GetPackets(new[] { packetId });
        }

        public async Task<GetPacketsResponse> GetPackets(IEnumerable<string> packetIds, CancellationToken token = default(CancellationToken))
        {
            if (!packetIds.Any()) return new GetPacketsResponse();

            var tasks = new List<Task<GetPacketsResponse>>();

            foreach (var batch in packetIds.Batch(Globals.MaxGetCount/5))
            {
                var request = new GetPackets(batch);

                var task = WebClient.Get<GetPacketsResponse>(request, Session, token);

                tasks.Add(task);
            }

            var responses = await Task.WhenAll(tasks).ConfigureAwait(false);

            var packets = responses.SelectMany(i => i.Packets);

            DecryptPackets(packets);

            return new GetPacketsResponse(packets);
        }

        public Task PostPackets(IEnumerable<Packet> packets, CancellationToken token = default(CancellationToken))
        {
            if (!packets.Any()) return Tasks.Complete;

            packets = packets.Select(i => (Packet)i.Clone()).ToList();

            EncryptPackets(packets);

            var request = new PostPackets(packets.Select(i => i.GetUpload()));

            return WebClient.Post<PostPacketsResponse>(request, Session, token);
        }

        public Task<GetNotificationsResponse> GetNotifications(DataCursor cursor, bool dependencies, CancellationToken token = default(CancellationToken))
        {
            var request = new GetNotifications(cursor, dependencies);

            return WebClient.Get<GetNotificationsResponse>(request, Session, token);
        }

        public Task<GetCounterResponse> GetCounter(string type, CancellationToken token = default(CancellationToken))
        {
            var request = new GetCounter(type);

            return WebClient.Get<GetCounterResponse>(request, Session, token);
        }

        public Task<ResetCounterResponse> ResetCounter(string type, CancellationToken token = default(CancellationToken))
        {
            var request = new ResetCounter(type);

            return WebClient.Delete<ResetCounterResponse>(request, Session, token);
        }

        public Task<GetResourcesResponse> GetResource(string resourceId, bool dependencies, CancellationToken token = default(CancellationToken))
        {
            return GetResources(new[] { resourceId }, dependencies);
        }

        public async Task<GetResourcesResponse> GetResources(IEnumerable<string> resourceIds, bool dependencies, CancellationToken token = default(CancellationToken))
        {
            if (!resourceIds.Any()) return new GetResourcesResponse();

            var tasks = new List<Task<GetResourcesResponse>>();

            foreach (var batch in resourceIds.Batch(Globals.MaxGetCount))
            {
                var request = new GetResources(batch, dependencies);

                var task = WebClient.Get<GetResourcesResponse>(request, Session, token);

                tasks.Add(task);
            }

            var responses = await Task.WhenAll(tasks).ConfigureAwait(false);

            if (dependencies)
            {
                return new GetResourcesResponse(responses.SelectMany(i => i.Resources), responses.SelectMany(i => i.Contacts));
            }
            else
            {
                return new GetResourcesResponse(responses.SelectMany(i => i.Resources));
            }
        }

        public Task<GetResourcesResponse> GetResources(DataCursor cursor, string type, bool dependencies, CancellationToken token = default(CancellationToken))
        {
            var request = new GetResources(cursor, type, dependencies);

            return WebClient.Get<GetResourcesResponse>(request, Session, token);
        }

        public async Task<PostResourceResponse> PostResource(string name, string label, string type, CancellationToken token = default(CancellationToken))
        {
            var request = new PostResource(name, label, type);

            return await WebClient.Post<PostResourceResponse>(request, Session, token).ConfigureAwait(false);
        }


        public async Task<PutResourceResponse> PutResource(string resourceId, string name, string label, string content, CancellationToken token = default(CancellationToken))
        {
            byte[] data = null;

            if (content != null)
            {
                data = await Gzip.Compress(content.ToBytes()).ConfigureAwait(false);
            }

            var request = new PutResource(resourceId, name, label, data);

            return await WebClient.Put<PutResourceResponse>(request, Session, token).ConfigureAwait(false);
        }

        public Task<DeleteResourceResponse> DeleteResource(string resourceId, CancellationToken token = default(CancellationToken))
        {
            var request = new DeleteResource(resourceId);

            return WebClient.Delete<DeleteResourceResponse>(request, Session, token);
        }

        public Task<PostResourceDependencyResponse> PostResourceDependency(string resourceId, string dependencyId, CancellationToken token = default(CancellationToken))
        {
            var request = new PostResourceDependency(resourceId, dependencyId);

            return WebClient.Post<PostResourceDependencyResponse>(request, Session, token);
        }

        public Task<DeleteResourceDependencyResponse> DeleteResourceDependency(string resourceId, string dependencyId, CancellationToken token = default(CancellationToken))
        {
            var request = new DeleteResourceDependency(resourceId, dependencyId);

            return WebClient.Delete<DeleteResourceDependencyResponse>(request, Session, token);
        }

        public Task<CloneResourceResponse> CloneResource(string modelId, string resourceId, CancellationToken token = default(CancellationToken))
        {
            var request = new CloneResource(modelId, resourceId);

            return WebClient.Post<CloneResourceResponse>(request, Session, token);
        }

        public Task<GetClassesResponse> GetClass(string classId, bool dependencies, CancellationToken token = default(CancellationToken))
        {
            return GetClasses(new[] { classId }, dependencies);
        }

        public async Task<GetClassesResponse> GetClasses(IEnumerable<string> classIds, bool dependencies, CancellationToken token = default(CancellationToken))
        {
            if (!classIds.Any()) return new GetClassesResponse();

            var tasks = new List<Task<GetClassesResponse>>();

            foreach (var batch in classIds.Batch(Globals.MaxGetCount))
            {
                var request = new GetClasses(batch);

                var task = WebClient.Get<GetClassesResponse>(request, Session, token);

                tasks.Add(task);
            }

            var responses = await Task.WhenAll(tasks).ConfigureAwait(false);

            return new GetClassesResponse(responses.SelectMany(i => i.Classes));
        }

        public Task<GetClassesResponse> GetClasses(DataCursor cursor, string portalId, bool includePost, bool dependencies, CancellationToken token = default(CancellationToken))
        {
            var request = new GetClasses(cursor, portalId, dependencies) { IncludePost = includePost };

            return WebClient.Get<GetClassesResponse>(request, Session, token);
        }

        public Task<GetModelsResponse> GetModel(string modelId, CancellationToken token = default(CancellationToken))
        {
            return GetModels(new[] { modelId });
        }

        public async Task<GetModelsResponse> GetModels(IEnumerable<string> modelIds, CancellationToken token = default(CancellationToken))
        {
            if (!modelIds.Any()) return new GetModelsResponse();

            var tasks = new List<Task<GetModelsResponse>>();

            foreach (var batch in modelIds.Batch(Globals.MaxGetCount))
            {
                var request = new GetModels(batch);

                var task = WebClient.Get<GetModelsResponse>(request, Session, token);

                tasks.Add(task);
            }

            var responses = await Task.WhenAll(tasks).ConfigureAwait(false);

            return new GetModelsResponse(responses.SelectMany(i => i.Models));
        }

        public Task<GetModelsResponse> GetModels(DataCursor cursor, string classId, CancellationToken token = default(CancellationToken))
        {
            var request = new GetModels(cursor, classId);

            return WebClient.Get<GetModelsResponse>(request, Session, token);
        }

        public Task<PostModelResponse> PostModel(string name, string label, string color, bool isInteractive, MediaUpload imageUpload, CancellationToken token = default(CancellationToken))
        {
            var request = new PostModel(name, label, color, isInteractive, imageUpload);

            return WebClient.Post<PostModelResponse>(request, Session, token);
        }

        public Task<PutModelResponse> PutModel(string modelId, string name, string label, string color, bool isInteractive, MediaUpload imageUpload, CancellationToken token = default(CancellationToken))
        {
            var request = new PutModel(modelId, name, label, color, isInteractive, imageUpload);

            return WebClient.Put<PutModelResponse>(request, Session, token);
        }

        public Task<DeleteModelResponse> DeleteModel(string modelId, CancellationToken token = default(CancellationToken))
        {
            var request = new DeleteModel(modelId);

            return WebClient.Delete<DeleteModelResponse>(request, Session, token);
        }

        public Task<CloneModelResponse> CloneModel(string modelId, string name, string label, string color, MediaUpload imageUpload, CancellationToken token = default(CancellationToken))
        {
            var request = new CloneModel(modelId, name, label, color, imageUpload);

            return WebClient.Post<CloneModelResponse>(request, Session, token);
        }

        public Task<PostModelDependencyResponse> PostModelDependency(string modelId, string dependencyId, CancellationToken token = default(CancellationToken))
        {
            var request = new PostModelDependency(modelId, dependencyId);

            return WebClient.Post<PostModelDependencyResponse>(request, Session, token);
        }

        public Task<PutModelDependencyResponse> PutModelDependency(string modelId, string dependencyId, CancellationToken token = default(CancellationToken))
        {
            var request = new PutModelDependency(modelId, dependencyId);

            return WebClient.Put<PutModelDependencyResponse>(request, Session, token);
        }

        public Task<DeleteModelDependencyResponse> DeleteModelDependency(string modelId, string dependencyId, CancellationToken token = default(CancellationToken))
        {
            var request = new DeleteModelDependency(modelId, dependencyId);

            return WebClient.Delete<DeleteModelDependencyResponse>(request, Session, token);
        }

        public Task<GetFormsResponse> GetForm(string formId, bool dependencies, CancellationToken token = default(CancellationToken))
        {
            return GetForms(new[] { formId }, dependencies);
        }

        public async Task<GetFormsResponse> GetForms(IEnumerable<string> formIds, bool dependencies, CancellationToken token = default(CancellationToken))
        {
            if (!formIds.Any()) return new GetFormsResponse();

            var tasks = new List<Task<GetFormsResponse>>();

            foreach (var batch in formIds.Batch(Globals.MaxGetCount))
            {
                var request = new GetForms(batch, dependencies);

                var task = WebClient.Get<GetFormsResponse>(request, Session, token);

                tasks.Add(task);
            }

            var responses = await Task.WhenAll(tasks).ConfigureAwait(false);

            if (dependencies)
            {
                return new GetFormsResponse(responses.SelectMany(i => i.Forms), responses.SelectMany(i=>i.Models).Distinct(i=>i.Key), responses.SelectMany(i => i.Classes).Distinct(i => i.Key));
            }
            else
            {
                return new GetFormsResponse(responses.SelectMany(i => i.Forms));
            }
        }

        public Task<SendFormResponse> SendForm(IEnumerable<QuestionUpload> questions, string contactId, CancellationToken token = default(CancellationToken))
        {
            var request = new SendForm(contactId, questions);

            return WebClient.Post<SendFormResponse>(request, Session, token);
        }

        public async Task<CompleteFormResponse> CompleteForm(string contactId, string formId, IEnumerable<Answer> answers, CancellationToken token = default(CancellationToken))
        {
            var messages = new List<SignalMessage>();

            foreach (var entity in answers.Select(i=>i.Entity).Where(i => !i.IsPublic).Distinct(i => i.Id))
            {
                var schema = await FileStore.GetSchema(entity.ModelId).ConfigureAwait(false);
                var cipher = new DataCipher(CryptoClient, Session.KeyStore.PrivateIdentityKey.Value, entity.Data, schema, null).Process();

                var keys = entity.Media.Where(i => i.KeyId != null).DistinctBy(i => i.KeyId).ToDictionary(i => i.KeyId, i => i.Key);
                var keyId = Key.GetId(KeyType.SecretKey, $"{entity.AccountId}.{entity.PacketId}");
                var key = await Session.KeyStore.GetKey(keyId, true).ConfigureAwait(false);
                keys.Add(entity.PacketId, key.Value);
                
                var packetData = CryptoClient.Encrypt(cipher.PrivateData.Serialize().ToBytes(), entity.PacketKey);
                var packet = new Packet(entity.PacketId, Session.Portals[SystemPortals.Profile], Session.AccountId, packetData);
                var payload = new EntityPayload(entity.PacketId, packetData, keys).Serialize().ToBytes();
                var packetMessages = await GenerateMessages(new[] { contactId }, SignalContentType.Entity, payload).ConfigureAwait(false);
                messages.AddRange(packetMessages);

                Session.PacketStore.Add(packet);
            }

            var request = new CompleteForm(formId, answers.Select(i=>i.GetUpload()), messages.Select(i=>i.GetUpload()));

            try
            {
                await Session.CryptoStore.SaveInserts(token).ConfigureAwait(false);

                var response = await WebClient.Put<CompleteFormResponse>(request, Session, token).ConfigureAwait(false);

                Session.CryptoStore.SaveDeletes(token).Run();

                return response;
            }
            catch (MismatchedEndpointsException e)
            {
                var endpoints = e.GetEndpoints();

                foreach(var extraEndpoint in endpoints.ExtraEndpoints)
                {
                    messages.RemoveAll(i=>i.Address == extraEndpoint.GetAddress());

                    Session.SessionStore.Delete(extraEndpoint.AccountId, extraEndpoint.DeviceId);
                }

                var senderKeyMessages = await GenerateSenderKeyDistributionMessages(endpoints.MissingEndpoints).ConfigureAwait(false);

                messages.AddRange(senderKeyMessages);

                request = new CompleteForm(formId, answers.Select(i => i.GetUpload()), messages.Select(i => i.GetUpload()));

                await Session.CryptoStore.SaveInserts(token).ConfigureAwait(false);

                var response = await WebClient.Put<CompleteFormResponse>(request, Session, token).ConfigureAwait(false);

                Session.CryptoStore.SaveDeletes(token).Run();

                return response;
            }
            finally
            {
                Session.CryptoStore.Reset();
            }
        }

        public Task<DeclineFormResponse> DeclineForm(string formId, CancellationToken token = default(CancellationToken))
        {
            var request = new DeclineForm(formId);

            return WebClient.Put<DeclineFormResponse>(request, Session, token);
        }

        public Task<GetPortalsResponse> GetPortal(string portalId, CancellationToken token = default(CancellationToken))
        {
            return GetPortals(new[] { portalId });
        }

        public async Task<GetPortalsResponse> GetPortals(IEnumerable<string> portalIds, CancellationToken token = default(CancellationToken))
        {
            if (!portalIds.Any()) return new GetPortalsResponse();

            var tasks = new List<Task<GetPortalsResponse>>();

            foreach (var batch in portalIds.Batch(Globals.MaxGetCount))
            {
                var request = new GetPortals(batch);

                var task = WebClient.Get<GetPortalsResponse>(request, Session, token);

                tasks.Add(task);
            }

            var responses = await Task.WhenAll(tasks).ConfigureAwait(false);

            return new GetPortalsResponse(responses.SelectMany(i => i.Portals));
        }

        public Task<GetPortalsResponse> GetPortals(DataCursor cursor, string contactId, string entityId, string[] types, string[] states, string mode, CancellationToken token = default(CancellationToken))
        {
            var request = new GetPortals(cursor, contactId, entityId, types, states, mode);

            return WebClient.Get<GetPortalsResponse>(request, Session, token);
        }

        public Task<PostPortalResponse> PostPortal(string type, string name, string description, string color, MediaUpload imageUpload, CancellationToken token = default(CancellationToken))
        {
            var request = new PostPortal(type, name, description, color, imageUpload);

            return WebClient.Post<PostPortalResponse>(request, Session, token);
        }

        public Task<PutPortalResponse> PutPortal( string portalId, string name, string description, string color, MediaUpload imageUpload, CancellationToken token = default(CancellationToken))
        {
            var request = new PutPortal(portalId, name, description, color, imageUpload);

            return WebClient.Put<PutPortalResponse>(request, Session, token);
        }

        public Task<DeletePortalResponse> DeletePortal(string portalId, CancellationToken token = default(CancellationToken))
        {
            var request = new DeletePortal() { Portal = portalId };

            return WebClient.Delete<DeletePortalResponse>(request, Session, token);
        }

        public Task<JoinPortalResponse> JoinPortal(string portalId, CancellationToken token = default(CancellationToken))
        {
            var request = new JoinPortal(portalId);

            return WebClient.Post<JoinPortalResponse>(request, Session, token);
        }

        public Task<LeavePortalResponse> LeavePortal(string portalId, CancellationToken token = default(CancellationToken))
        {
            var request = new LeavePortal(portalId);

            return WebClient.Post<LeavePortalResponse>(request, Session, token);
        }

        public Task<GetFeedbackResponse> GetFeedback(DataCursor cursor, string appName, string type, bool dependencies, CancellationToken token = default(CancellationToken))
        {
            var request = new GetFeedback(cursor, appName, type, dependencies);

            return WebClient.Get<GetFeedbackResponse>(request, Session, token);
        }

        public Task<PostFeedbackResponse> PostFeedback(string appName, string type, string label, string description, CancellationToken token = default(CancellationToken))
        {
            var request = new PostFeedback(appName, type, label, description);

            return WebClient.Post<PostFeedbackResponse>(request, Session, token);
        }

        public Task<PutFeedbackResponse> PutFeedback(string feedbackId, string label, string description, string state, string response, CancellationToken token = default(CancellationToken))
        {
            var request = new PutFeedback(feedbackId, label, description, state, response);

            return WebClient.Put<PutFeedbackResponse>(request, Session, token);
        }

        public Task<DeleteFeedbackResponse> DeleteFeedback(string feedbackId, CancellationToken token = default(CancellationToken))
        {
            var request = new DeleteFeedback(feedbackId);

            return WebClient.Delete<DeleteFeedbackResponse>(request, Session, token);
        }

        public Task<VoteFeedbackResponse> VoteFeedback(string feedbackId, CancellationToken token = default(CancellationToken))
        {
            var request = new VoteFeedback(feedbackId);

            return WebClient.Put<VoteFeedbackResponse>(request, Session, token);
        }

        #region Crypto Stuff

        void EncryptKeys(IEnumerable<Key> keys, byte[] passwordKey, byte[] masterKey)
        {
            foreach(var key in keys)
            {
                switch(key.Type)
                {
                    case KeyType.MasterKey:
                        key.Value = CryptoClient.Encrypt(key.Value, passwordKey);
                        break;
                    case KeyType.PublishedIdentityKey:
                    case KeyType.PublishedSignedPreKey:
                    case KeyType.PublishedOneTimePreKey:
                        break;
                    default:
                        key.Value = CryptoClient.Encrypt(key.Value, masterKey);
                        break;
                }
            }
        }

        void DecryptKeys(IEnumerable<Key> keys)
        {
            foreach (var key in keys)
            {
                if (key.IsEncrypted)
                {
                    try
                    {
                        switch (key.Type)
                        {
                            case KeyType.MasterKey:
                                key.Value = CryptoClient.Decrypt(key.Value, Session.KeyStore.PasswordKey.Value);
                                break;
                            case KeyType.PublishedIdentityKey:
                            case KeyType.PublishedSignedPreKey:
                            case KeyType.PublishedOneTimePreKey:
                                break;
                            default:
                                key.Value = CryptoClient.Decrypt(key.Value, Session.KeyStore.MasterKey.Value);
                                break;
                        }
                        key.IsEncrypted = false;
                    }
                    catch { }
                }
            }
        }

        void EncryptPackets(IEnumerable<Packet> packets)
        {
            foreach (var packet in packets)
            {
                if (!packet.IsEncrypted)
                {
                    packet.Data = CryptoClient.Encrypt(packet.Data, Session.KeyStore.MasterKey.Value);

                    packet.IsEncrypted = true;
                }
            }
        }

        void DecryptPackets(IEnumerable<Packet> packets)
        {
            foreach(var packet in packets)
            {
                if (packet.IsEncrypted)
                {
                    packet.Data = CryptoClient.Decrypt(packet.Data, Session.KeyStore.MasterKey.Value);

                    packet.IsEncrypted = false;
                }
            }
        }

        public IEnumerable<KeyUpload> GenerateRegistrationKeys(byte[] passwordKey, KeyPair identityKeyPair)
        {
            var masterKey = CryptoClient.GenerateSecretKey();

            var preKey = CryptoClient.GenerateSignedPreKey(identityKeyPair.PrivateKey);

            var keys = new List<Key>();

            keys.Add(new Key(null, KeyType.MasterKey, masterKey));
            keys.Add(new Key(null, KeyType.PublishedIdentityKey, identityKeyPair.PublicKey));
            keys.Add(new Key(null, KeyType.PublicIdentityKey, identityKeyPair.PublicKey));
            keys.Add(new Key(null, KeyType.PrivateIdentityKey, identityKeyPair.PrivateKey));
            keys.Add(new Key("1", KeyType.PublishedSignedPreKey, preKey.PublicKey, preKey.Signature));
            keys.Add(new Key("1", KeyType.PublicSignedPreKey, preKey.PublicKey, preKey.Signature));
            keys.Add(new Key("1", KeyType.PrivateSignedPreKey, preKey.PrivateKey, preKey.Signature));

            keys.AddRange(GenerateOneTimePreKeys());

            EncryptKeys(keys, passwordKey, masterKey);

            return keys.Select(i => i.GetUpload());
        }

        public List<Key> GenerateOneTimePreKeys()
        {
            var keys = new List<Key>();
            var oneTimePreKeys = CryptoClient.GeneratePreKeys(Globals.PreKeyBatchSize);
            var id = 1;
            foreach (var oneTimePreKey in oneTimePreKeys)
            {
                keys.Add(new Key(id.ToString(), KeyType.PublishedOneTimePreKey, oneTimePreKey.PublicKey));
                keys.Add(new Key(id.ToString(), KeyType.PublicOneTimePreKey, oneTimePreKey.PublicKey));
                keys.Add(new Key(id.ToString(), KeyType.PrivateOneTimePreKey, oneTimePreKey.PrivateKey));
                id++;
            }
            return keys;
        }

        public async Task<List<SignalMessage>> GenerateMessages(IEnumerable<string> portals, string type, byte[] content)
        {
            var messages = new List<SignalMessage>();
            using (var cipher = CryptoClient.GetSignalCipher(Session))
            {
                foreach (var portalId in portals)
                {
                    var message = await cipher.GenerateSenderKeyMessage(portalId, type, content).ConfigureAwait(false);
                    messages.Add(message);
                }
            }
            return messages;
        }

        public async Task<List<SignalMessage>> GenerateSenderKeyDistributionMessages(IEnumerable<SignalEndpoint> endpoints)
        {
            var messages = new List<SignalMessage>();
            using (var cipher = CryptoClient.GetSignalCipher(Session))
            {
                foreach (var endpoint in endpoints)
                {
                    var message = await cipher.GenerateSenderKeyDistributionMessage(endpoint.PortalId, endpoint.AccountId, endpoint.DeviceId).ConfigureAwait(false);
                    messages.Add(message);
                }
            }
            return messages;
        }

        public async Task ProcessMessages(List<SignalMessage> messages, CancellationToken token)
        {
            using (var cipher = CryptoClient.GetSignalCipher(Session))
            {
                foreach (var message in messages)
                {
                    try
                    {
                        await cipher.ProcessMessage(message).ConfigureAwait(false);
                    }
                    catch (Exception e) when (!(e is ApiException))
                    {
                        System.Diagnostics.Debugger.Break();
                    }
                }

                await Session.CryptoStore.SaveInserts(token).ConfigureAwait(false);
                await Session.CryptoStore.SaveDeletes(token, false).ConfigureAwait(false);
                Session.CryptoStore.Reset();
            }
        }
    }
    #endregion
}
