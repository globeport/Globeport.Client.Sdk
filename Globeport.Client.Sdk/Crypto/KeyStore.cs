using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Globeport.Shared.Library.ClientModel;
using Globeport.Shared.Library.Data;
using Globeport.Shared.Library.Components;
using Globeport.Shared.Library.Extensions;
using Globeport.Client.Sdk.Exceptions;

namespace Globeport.Client.Sdk.Crypto
{
    public class KeyStore
    {
        public ApiClient ApiClient { get; set; }
        public Key PasswordKey { get; set; }
        public Key MasterKey { get; set; }
        public Key PublicIdentityKey { get; set; }
        public Key PrivateIdentityKey { get; set; }
        public Dictionary<string, Key> AddedKeys { get; private set; }
        public HashSet<string> DeletedKeys { get; set; }
        public Dictionary<string, Key> StoredKeys { get; } = new Dictionary<string, Key>();

        public KeyStore(ApiClient apiClient)
        {
            ApiClient = apiClient;
            Reset();
        }

        public virtual Task Initialize()
        {
            return Tasks.Complete;
        }

        public void Initialize(IEnumerable<Key> keys)
        {
            foreach (var key in keys)
            {
                switch (key.Type)
                {
                    case KeyType.PasswordKey:
                        PasswordKey = key;
                        break;
                    case KeyType.MasterKey:
                        MasterKey = key;
                        break;
                    case KeyType.PublicIdentityKey:
                        PublicIdentityKey = key;
                        break;
                    case KeyType.PrivateIdentityKey:
                        PrivateIdentityKey = key;
                        break;
                }
            }
        }

        public void Add(Key key)
        {
            var id = key.GetId();
            DeletedKeys.Remove(id);
            AddedKeys[id] = key;
        }

        public void Add(IEnumerable<Key> keys)
        {
            foreach(var key in keys)
            {
                Add(key);
            }
        }

        public void Delete(Key key)
        {
            var id = key.GetId();
            AddedKeys.Remove(id);
            DeletedKeys.Add(id);
        }

        public async Task<Key> GetKey(string id, bool throwOnNotFound = false)
        {
            var key = AddedKeys.GetValue(id);
            if (key == null)
            {
                key = await GetStoredKey(id).ConfigureAwait(false);
            }
            if (key == null)
            {
                var response = await ApiClient.GetKey(id).ConfigureAwait(false);
                key = response.Keys.FirstOrDefault();
                if (key == null && throwOnNotFound)
                {
                    throw new NotFoundException();
                }
                else if (key != null)
                {
                    await PutStoredKey(key).ConfigureAwait(false);
                }
            }
            return key;
        }

        protected virtual Task PutStoredKey(Key key)
        {
            StoredKeys.Add(key.GetId(), key);

            return Tasks.Complete;
        }

        protected virtual Task<Key> GetStoredKey(string id)
        {
            return Task.FromResult(StoredKeys.GetValue(id));
        }

        public async Task<KeyBundle> GetKeyBundle(string accountId)
        {
            var response = await ApiClient.GetKeyBundle(accountId).ConfigureAwait(false);
            var keys = response.KeyBundle;
            var identityKey = await GetKey(Key.GetId(KeyType.PublicIdentityKey, accountId)).ConfigureAwait(false);
            if (identityKey != null && !identityKey.Value.SequenceEqual(keys.IdentityKey.Value))
            {
                //uh oh, the published identity key differs from the one we trusted
                throw new Exception("Identity key not trusted");
            }
            else if (identityKey == null)
            {
                //this is the point of trust
                keys.IdentityKey.Type = KeyType.PublicIdentityKey;
                Add(keys.IdentityKey);
            }
            return keys;
        }

        public void Reset()
        {
            AddedKeys = new Dictionary<string, Key>();
            DeletedKeys = new HashSet<string>();
        }
    }
}
