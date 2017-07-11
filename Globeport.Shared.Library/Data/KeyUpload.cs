using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Globeport.Shared.Library.Extensions;

namespace Globeport.Shared.Library.Data
{
    public class KeyUpload
    {
        public string KeyId { get; set; }
        public string Type { get; set; }
        public byte[] Value { get; set; }
        public byte[] Signature { get; set; }

        public KeyUpload()
        {
        }

        public KeyUpload(string keyId, string type, byte[] value, byte[] signature = null)
        {
            KeyId = keyId;
            Type = type;
            Value = value;
            Signature = signature;
        }

        public bool IsMasterKey()
        {
            return Type == KeyType.MasterKey;
        }

        public bool IsSenderKey()
        {
            return Type == KeyType.SenderKey;
        }

        public bool IsSecretKey()
        {
            return Type == KeyType.SecretKey;
        }

        public bool IsIdentityKey()
        {
            return Type.In
            (
                KeyType.PublishedIdentityKey,
                KeyType.PublicIdentityKey,
                KeyType.PrivateIdentityKey
            );
        }

        public bool IsOneTimePreKey()
        {
            return Type.In
            (
                KeyType.PublishedOneTimePreKey,
                KeyType.PublicOneTimePreKey,
                KeyType.PrivateOneTimePreKey
            );
        }

        public bool IsPreKey()
        {
            return Type.In
            (
                KeyType.PublishedSignedPreKey,
                KeyType.PublicSignedPreKey,
                KeyType.PrivateSignedPreKey,
                KeyType.PublishedOneTimePreKey,
                KeyType.PublicOneTimePreKey,
                KeyType.PrivateOneTimePreKey
            );
        }

        public bool IsPublishedKey()
        {
            return Type.In
            (
               KeyType.PublishedIdentityKey,
               KeyType.PublishedSignedPreKey,
               KeyType.PublishedOneTimePreKey
            );
        }

        public bool IsPublicKey()
        {
            return Type.In
            (
                 KeyType.PublicIdentityKey,
                 KeyType.PublicSignedPreKey,
                 KeyType.PublicOneTimePreKey
            );
        }

        public bool IsPrivateKey()
        {
            return Type.In
            (
                KeyType.PrivateIdentityKey,
                KeyType.PrivateSignedPreKey,
                KeyType.PrivateOneTimePreKey
            );
        }

        public bool IsSignedPreKey()
        {
            return Type.In
            (
                KeyType.PublishedSignedPreKey,
                KeyType.PublicSignedPreKey,
                KeyType.PrivateSignedPreKey
            );
        }
    }
}
