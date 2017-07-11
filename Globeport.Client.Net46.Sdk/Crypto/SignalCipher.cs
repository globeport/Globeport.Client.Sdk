using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using libsignal;
using libsignal.state;
using libsignal.ecc;
using libsignal.groups;
using protocol = libsignal.protocol;
using Strilanc.Value;

using Globeport.Shared.Library.ClientModel;
using Globeport.Shared.Library.Data;
using Globeport.Shared.Library.Extensions;
  
namespace Globeport.Client.Sdk.Crypto
{
    public class SignalCipher : ISignalCipher
    {
        public ApiSession Session { get; private set; }
        public KeyStore KeyStore { get; private set; }
        public PacketStore PacketStore { get; private set; }
        public SessionStore SessionStore { get; private set; }
        public SignalStore SignalStore { get; private set; }

        public SignalCipher(ApiSession session)
        {
            Session = session;
            KeyStore = session.KeyStore;
            SessionStore = session.SessionStore;
            PacketStore = session.PacketStore;
            var publicKey = new IdentityKey(Curve.decodePoint(KeyStore.PublicIdentityKey.Value, 0));
            var privateKey = Curve.decodePrivatePoint(KeyStore.PrivateIdentityKey.Value);
            SignalStore = new SignalStore(new IdentityKeyPair(publicKey, privateKey));
            SignalStore.SessionStore.SessionStored += SessionStore_SessionStored;
        }

        public async Task<SignalMessage> GenerateMessage(string portalId, string contactId, long deviceId, string contentType, byte[] content)
        {
            var address = new SignalProtocolAddress(contactId, (uint)deviceId);
            var session = await SessionStore.GetSession(contactId, deviceId).ConfigureAwait(false);
            if (session.IsEmpty())
            {
                var keys = await KeyStore.GetKeyBundle(contactId).ConfigureAwait(false);
                var sessionBuilder = new SessionBuilder(SignalStore, address);
                //one time pre key can be null;
                var oneTimePreKeyId = uint.Parse(keys.OneTimePreKey?.KeyId);
                var oneTimePreKey = keys.OneTimePreKey?.Value != null ? Curve.decodePoint(keys.OneTimePreKey?.Value, 0) : null;
                var identityKey = new IdentityKey(Curve.decodePoint(keys.IdentityKey.Value, 0));
                var preKeyBundle = new PreKeyBundle(0, (uint)session.DeviceId, oneTimePreKeyId, oneTimePreKey, uint.Parse(keys.SignedPreKey.KeyId), Curve.decodePoint(keys.SignedPreKey.Value, 0), keys.SignedPreKey.Signature, identityKey);
                sessionBuilder.process(preKeyBundle);
            }
            else
            {
                SignalStore.StoreSession(address, new SessionRecord(session.Data));
            }
            var cipher = new SessionCipher(SignalStore, address);
            var message = cipher.encrypt(content);

            //dont store session for floating session
            if (deviceId == 0) SessionStore.Delete(contactId, deviceId);

            return new SignalMessage(SignalMessage.GetAddress(contactId, deviceId, portalId), GetSignalMessageType(message.getType()), contentType, message.serialize());
        }

        public string GetSignalMessageType(uint type)
        {
            switch(type)
            {
                case protocol.CiphertextMessage.PREKEY_TYPE:
                    return SignalMessageType.PreKey;
                case protocol.CiphertextMessage.SENDERKEY_TYPE:
                    return SignalMessageType.SenderKey;
            }
            throw new Exception("Unrecognized signal message type");
        }

        public async Task<SignalMessage> GenerateSenderKeyDistributionMessage(string portalId, string contactId, long deviceId)
        {
            var sender = new SignalProtocolAddress(Session.AccountId, (uint)Session.DeviceId);
            var senderKeyName = new SenderKeyName(portalId, sender);
            var senderKey = await KeyStore.GetKey(Key.GetId(KeyType.SenderKey, senderKeyName.serialize().Replace("::", "."))).ConfigureAwait(false);
            if (senderKey != null)
            {
                SignalStore.SenderKeyStore.storeSenderKey(senderKeyName, new SenderKeyRecord(senderKey.Value));
                var key = SignalStore.SenderKeyStore.loadSenderKey(senderKeyName).serialize();
                KeyStore.Add(new Key(senderKeyName.serialize().Replace("::", "."), KeyType.SenderKey, key));
            }
            var content = new GroupSessionBuilder(SignalStore.SenderKeyStore).create(senderKeyName).serialize();
            var message = await GenerateMessage(portalId, contactId, deviceId, SignalContentType.SenderKey, content).ConfigureAwait(false);
            return message;
        }

        public async Task<SignalMessage> GenerateSenderKeyMessage(string groupId, string contentType, byte[] content)
        {
            var sender = new SignalProtocolAddress(Session.AccountId, (uint)Session.DeviceId);
            var senderKeyName = new SenderKeyName(groupId, sender);
            var senderKey = await KeyStore.GetKey(Key.GetId(KeyType.SenderKey, senderKeyName.serialize().Replace("::", "."))).ConfigureAwait(false);
            if (senderKey == null) 
            {
                new GroupSessionBuilder(SignalStore.SenderKeyStore).create(senderKeyName);
                var key = SignalStore.SenderKeyStore.loadSenderKey(senderKeyName).serialize();
                KeyStore.Add(new Key(senderKeyName.serialize().Replace("::", "."), KeyType.SenderKey, key));
            }
            else
            {
                SignalStore.SenderKeyStore.storeSenderKey(senderKeyName, new SenderKeyRecord(senderKey.Value));
            }
            var groupCipher = new GroupCipher(SignalStore.SenderKeyStore, new SenderKeyName(groupId, sender));
            var message = groupCipher.encrypt(content);
            
            return new SignalMessage(groupId, SignalMessageType.SenderKey, contentType, message);
        }

        public async Task ProcessMessage(SignalMessage message)
        {
            byte[] content;
            switch (message.MessageType)
            {
                case SignalMessageType.PreKey:
                    content = await DecryptPreKeyMessage(message).ConfigureAwait(false);
                    ProcessMessageContent(message, content);
                    break;
                case SignalMessageType.SenderKey:
                    content = await DecryptSenderKeyMessage(message, message.GetPortalId()).ConfigureAwait(false);
                    ProcessMessageContent(message, content);
                    break;
                case SignalMessageType.System:
                    ProcessMessageContent(message, message.Content);
                    break;
            }
        }

        async Task<byte[]> DecryptPreKeyMessage(SignalMessage message)
        {
            var contactId = message.GetContactId();
            var deviceId = message.GetDeviceId();

            var cipher = await GetSessionCipher(contactId, deviceId).ConfigureAwait(false);

            var preKeyMessage = new protocol.PreKeySignalMessage(message.Content);

            var sessionRecord = SignalStore.LoadSession(new SignalProtocolAddress(contactId, (uint)deviceId));

            if (!sessionRecord.hasSessionState(preKeyMessage.getMessageVersion(), preKeyMessage.getBaseKey().serialize()))
            {
                var signedPreKeyId = preKeyMessage.getSignedPreKeyId();
                var t1 = KeyStore.GetKey(Key.GetId(KeyType.PublicSignedPreKey, signedPreKeyId.ToString()), true);
                var t2 = KeyStore.GetKey(Key.GetId(KeyType.PrivateSignedPreKey, signedPreKeyId.ToString()), true);
                await Task.WhenAll(t1, t2).ConfigureAwait(false);
                var publicSignedPreKey = t1.Result;
                var privateSignedPreKey = t2.Result;
                var signedKeyPair = new ECKeyPair(Curve.decodePoint(publicSignedPreKey.Value, 0), Curve.decodePrivatePoint(privateSignedPreKey.Value));

                SignalStore.StoreSignedPreKey(signedPreKeyId, new SignedPreKeyRecord(signedPreKeyId, 0, signedKeyPair, publicSignedPreKey.Signature));

                if (preKeyMessage.getPreKeyId().HasValue)
                {
                    var preKeyId = preKeyMessage.getPreKeyId().ForceGetValue();
                    t1 = KeyStore.GetKey(Key.GetId(KeyType.PublicOneTimePreKey, preKeyId.ToString()), true);
                    t2 = KeyStore.GetKey(Key.GetId(KeyType.PrivateOneTimePreKey, preKeyId.ToString()), true);
                    await Task.WhenAll(t1, t2).ConfigureAwait(false);
                    var publicOneTimePreKey = t1.Result;
                    var privateOneTimePreKey = t2.Result;
                    var oneTimeKeyPair = new ECKeyPair(Curve.decodePoint(publicOneTimePreKey.Value, 0), Curve.decodePrivatePoint(privateOneTimePreKey.Value));

                    SignalStore.StorePreKey(preKeyId, new PreKeyRecord(preKeyId, oneTimeKeyPair));

                    KeyStore.Delete(publicOneTimePreKey);
                    KeyStore.Delete(privateOneTimePreKey);  
                }
            }

            var result = cipher.decrypt(preKeyMessage);

            //dont store session for these messages
            if (message.IsFloating) SessionStore.Delete(contactId, deviceId);

            return result;
        }

        async Task<byte[]> DecryptSenderKeyMessage(SignalMessage message, string groupId)
        {
            var sender = new SignalProtocolAddress(message.GetContactId(), (uint)message.GetDeviceId());
            var senderKeyName = new SenderKeyName(groupId, sender);
            var senderKey = await KeyStore.GetKey(Key.GetId(KeyType.SenderKey, $"{senderKeyName.serialize().Replace("::", ".")}"), true).ConfigureAwait(false);
            SignalStore.SenderKeyStore.storeSenderKey(senderKeyName, new SenderKeyRecord(senderKey.Value));
            var groupCipher = new GroupCipher(SignalStore.SenderKeyStore, senderKeyName);

            return groupCipher.decrypt(message.Content);
        }

        void ProcessMessageContent(SignalMessage message, byte[] content)
        {
            switch (message.ContentType)
            {
                case SignalContentType.Entity:
                    var payload = content.FromBytes().Deserialize<EntityPayload>();
                    KeyStore.Add(payload.Keys.Select(i => new Key($"{message.GetContactId()}.{i.Key}", KeyType.SecretKey, i.Value)));
                    PacketStore.Add(new Packet(payload.Id, message.GetPortalId(), message.GetContactId(), payload.Content));
                    break;
                case SignalContentType.SenderKey:
                    var sender = new SignalProtocolAddress(message.GetContactId(), (uint)message.GetDeviceId());
                    var senderKeyName = new SenderKeyName(message.GetPortalId(), sender);
                    var groupKeyMessage = new protocol.SenderKeyDistributionMessage(content);
                    var groupSession = new GroupSessionBuilder(SignalStore.SenderKeyStore);
                    groupSession.process(senderKeyName, groupKeyMessage);
                    var senderKey = SignalStore.SenderKeyStore.loadSenderKey(senderKeyName).serialize();
                    KeyStore.Add(new Key($"{senderKeyName.serialize().Replace("::", ".")}", KeyType.SenderKey, senderKey));
                    break;
                case SignalContentType.Command:
                    var command = content.FromBytes().Deserialize<SignalCommand>();
                    ProcessCommand(message, command);
                    break;
            }
        }

        void ProcessCommand(SignalMessage message, SignalCommand command)
        {
            switch (command.Command)
            {
                case SignalCommands.DeletePacket:
                    PacketStore.Delete(Packet.GetId(command.Parameters[0], message.GetContactId(), command.Parameters[1]));
                    break;
                case SignalCommands.DeleteContainer:
                    PacketStore.DeleteByContainer(command.Parameters[0]);
                    break;
            }
        }

        async Task<SessionCipher> GetSessionCipher(string contactId, long deviceId)
        {
            var address = new SignalProtocolAddress(contactId, (uint)deviceId);
            var session = await SessionStore.GetSession(contactId, deviceId).ConfigureAwait(false);
            if (session.IsEmpty())
            {
                var sessionBuilder = new SessionBuilder(SignalStore, address);
            }
            else
            {
                SignalStore.StoreSession(address, new SessionRecord(session.Data));
            }
            return new SessionCipher(SignalStore, address);
        }

        public void Dispose()
        {
            SignalStore.SessionStore.SessionStored -= SessionStore_SessionStored;
        }

        private void SessionStore_SessionStored(object sender, SignalProtocolAddress address)
        {
            SessionStore.Update(address.getName(), address.getDeviceId(), SignalStore.SessionStore.SessionRecords[address.ToString()].serialize());
        }
    }
}
