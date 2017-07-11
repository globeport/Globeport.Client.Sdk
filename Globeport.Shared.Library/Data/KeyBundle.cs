using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Globeport.Shared.Library.ClientModel;

namespace Globeport.Shared.Library.Data
{
    public class KeyBundle
    {
        public Key IdentityKey { get; set; }
        public Key SignedPreKey { get; set; }
        public Key OneTimePreKey { get; set; }

        public KeyBundle(Key identityKey, Key signedPreKey, Key oneTimePreKey)
        {
            IdentityKey = identityKey;
            SignedPreKey = signedPreKey;
            OneTimePreKey = oneTimePreKey;
        }
    }
}
