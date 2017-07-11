using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Globeport.Client.Sdk.Crypto;

namespace Globeport.Client.Sdk
{
    public class StoreFactory
    {
        public virtual CryptoStore CreateCryptoStore(ApiClient apiClient)
        {
            var keyStore = new KeyStore(apiClient);
            var packetStore = new PacketStore(apiClient);
            var sessionStore = new SessionStore();
            return new CryptoStore(apiClient, keyStore, packetStore, sessionStore);
        }

        public virtual FileStore CreateFileStore(ApiClient apiClient)
        {
            return new FileStore(apiClient);
        }
    }
}
