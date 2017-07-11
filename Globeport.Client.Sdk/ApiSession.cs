using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Globeport.Shared.Library.Interfaces;
using Globeport.Shared.Library.ClientModel;
using Globeport.Client.Sdk.Crypto;

namespace Globeport.Client.Sdk
{
    public class ApiSession : ISession
    {
        public string AccountId { get; set; }
        public long DeviceId { get; set; }
        public string SessionId { get; set; }
        public string Username { get; set; }
        public byte[] SessionKey { get; set; }
        public byte[] Salt { get; set; }
        public bool IsAuthenticated { get; set; }
        public Dictionary<string, string> Portals { get; set; }
        public CryptoStore CryptoStore { get; set; }
        public KeyStore KeyStore { get; set; }
        public SessionStore SessionStore { get; set; }
        public PacketStore PacketStore { get; set; }

        public ApiSession(CryptoStore cryptoStore)
        {
            CryptoStore = cryptoStore;
            KeyStore = cryptoStore.KeyStore;
            SessionStore = cryptoStore.SessionStore;
            PacketStore = cryptoStore.PacketStore;
        }

        public Task Initialize(Account account)
        {
            Configure(account);
            return KeyStore.Initialize();
        }

        public void Initialize(Account account, IEnumerable<Key> keys)
        {
            Configure(account);
            KeyStore.Initialize(keys);
        }

        void Configure(Account account)
        {
            AccountId = account.Id;
            DeviceId = account.DeviceId;
            Salt = account.Salt;
            SessionId = account.SessionId;
            SessionKey = account.SessionKey;
            IsAuthenticated = account.IsAuthenticated;
            Username = account.Username;
            Portals = account.Portals;
        }
    }
}
