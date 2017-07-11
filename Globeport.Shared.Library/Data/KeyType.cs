using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Globeport.Shared.Library.Data
{
    public static class KeyType
    {
        public const string PasswordKey = nameof(PasswordKey);
        public const string MasterKey = nameof(MasterKey);
        public const string PublishedIdentityKey = nameof(PublishedIdentityKey);
        public const string PublicIdentityKey = nameof(PublicIdentityKey);
        public const string PrivateIdentityKey = nameof(PrivateIdentityKey);
        public const string PublishedSignedPreKey = nameof(PublishedSignedPreKey);
        public const string PublicSignedPreKey = nameof(PublicSignedPreKey);
        public const string PrivateSignedPreKey = nameof(PrivateSignedPreKey);
        public const string PublishedOneTimePreKey = nameof(PublishedOneTimePreKey);
        public const string PublicOneTimePreKey = nameof(PublicOneTimePreKey);
        public const string PrivateOneTimePreKey = nameof(PrivateOneTimePreKey);
        public const string SenderKey = nameof(SenderKey);
        public const string SecretKey = nameof(SecretKey);
    }
}
