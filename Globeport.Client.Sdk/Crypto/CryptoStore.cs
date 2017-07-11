using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Globeport.Shared.Library.Components;
using Globeport.Shared.Library.Data;
using Globeport.Shared.Library.Extensions;

namespace Globeport.Client.Sdk.Crypto
{
    public class CryptoStore
    {
        public ApiClient ApiClient { get; }
        public KeyStore KeyStore { get; protected set; }
        public PacketStore PacketStore { get; protected set; }
        public SessionStore SessionStore { get; protected set; }

        public CryptoStore(ApiClient apiClient, KeyStore keyStore, PacketStore packetStore, SessionStore sessionStore)
        {
            ApiClient = apiClient;
            KeyStore = keyStore;
            PacketStore = packetStore;
            SessionStore = sessionStore;
        }

        public virtual async Task SaveInserts(CancellationToken token)
        {
            await SaveRemoteInserts(token).ConfigureAwait(false);
            await SaveLocalInserts(token).ConfigureAwait(false);
        }

        protected virtual Task SaveLocalInserts(CancellationToken token)
        {
            PacketStore.StoredPackets.AddRange(PacketStore.AddedPackets);
            KeyStore.StoredKeys.AddRange(KeyStore.AddedKeys);
            SessionStore.StoredSessions.AddRange(SessionStore.UpdatedSessions);
            return Tasks.Complete;
        }

        protected Task SaveRemoteInserts(CancellationToken token)
        {
            var keys = KeyStore.AddedKeys.Values.Where(i => i.Type != KeyType.SenderKey || i.KeyId.Split('.')[1] != ApiClient.Session.AccountId);

            var t1 = ApiClient.PostKeys(keys, token);
            var t2 = ApiClient.PostPackets(PacketStore.AddedPackets.Values, token);

            return Task.WhenAll(t1, t2);
        }

        public async Task SaveDeletes(CancellationToken token, bool ignoreErrors = true)
        {
            try
            {
                await SaveLocalDeletes(token).ConfigureAwait(false);
                await SaveRemoteDeletes(token).ConfigureAwait(false);
            }
            catch
            {
                if (ignoreErrors) return;

                throw;
            }
        }

        protected virtual Task SaveLocalDeletes(CancellationToken token)
        {
            KeyStore.StoredKeys.RemoveAll(i => KeyStore.DeletedKeys.Contains(i.Key));
            SessionStore.StoredSessions.RemoveAll(i => SessionStore.DeletedSessions.Contains(i.Key));
            PacketStore.StoredPackets.RemoveAll(i => PacketStore.DeletedPackets.Contains(i.Key));
            PacketStore.StoredPackets.RemoveAll(i => PacketStore.DeletedContainers.Contains(i.Value.ContainerId));
            return Tasks.Complete;
        }

        protected Task SaveRemoteDeletes(CancellationToken token)
        {
            //delete keys from cloud provider
            return ApiClient.DeleteKeys(KeyStore.DeletedKeys, token);

            //delete packets from cloud provider
            //to do: implement this when alternative cloud providers added
        }

        public void Reset()
        {
            PacketStore.Reset();
            KeyStore.Reset();
            SessionStore.Reset();
        }
    }
}
