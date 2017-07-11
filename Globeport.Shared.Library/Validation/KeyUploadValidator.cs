using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FluentValidation;

using Globeport.Shared.Library.Extensions;
using Globeport.Shared.Library.Data;

namespace Globeport.Shared.Library.Validation
{
    public class KeyUploadValidator: AbstractValidator<KeyUpload>
    {
        public const int MasterKeyLength = 72;
        public const int PublishedKeyLength = 33;
        public const int PublicKeyLength = 73;
        public const int PrivateKeyLength = 72;
        public const int SecretKeyLength = 32;

        public HashSet<string> OneTimePreKeyIds { get; } = new HashSet<string>(Enumerable.Range(1, 100).Select(i => i.ToString()));
        public string SignedPreKeyId { get; } = "1";

        public KeyUploadValidator(bool isSignUp)
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(i => i.Type).NotNull().Must(i => typeof(KeyType).GetConstants().ContainsKey(i));
            RuleFor(i => i.KeyId).NotNull().Must(IsValidPreKeyId).When(i=>i.IsPreKey());
            RuleFor(i => i.KeyId).Must(IsValidMasterKeyId).When(i => i.IsMasterKey());
            RuleFor(i => i.KeyId).Must(i=> IsValidIdentityKeyId(i, isSignUp)).When(i => i.IsIdentityKey());
            RuleFor(i => i.KeyId).NotNull().Must(IsValidSenderKeyId).When(i => i.IsSenderKey());
            RuleFor(i => i.KeyId).NotNull().Must(IsValidSecretKeyId).When(i => i.IsSecretKey());
            RuleFor(i => i.Value).NotNull().Must(i => i.Length == MasterKeyLength).When(i => i.IsMasterKey());
            RuleFor(i => i.Value).NotNull().Must(i => i.Length == (isSignUp ? PublishedKeyLength : PublicKeyLength)).When(i => i.IsPublishedKey());
            RuleFor(i => i.Value).NotNull().Must(i => i.Length == PublicKeyLength).When(i => i.IsPublicKey());
            RuleFor(i => i.Value).NotNull().Must(i => i.Length == PrivateKeyLength).When(i => i.IsPrivateKey());
            RuleFor(i => i.Signature).NotNull().Must(Validators.IsValidSignature).When(i => i.IsSignedPreKey());
            RuleFor(i => i.Signature).Null().When(i => !i.IsSignedPreKey());
        }

        public static bool IsValidIdentityKeyId(string id, bool isSignUp)
        {
            if (isSignUp)
            {
                return id == null;
            }
            else
            {
                return Validators.IsValidId(id);
            }
        }

        public static bool IsValidMasterKeyId(string id)
        {
            return id == null;
        }

        public static bool IsValidSenderKeyId(string id)
        {
            var parts = id.Split('.');
            if (parts.Length != 3) return false;
            if (!Validators.IsValidId(parts[0])) return false;
            if (!Validators.IsValidId(parts[1])) return false;
            if (!parts[2].IsInteger()) return false;
            return true;
        }

        public static bool IsValidSecretKeyId(string id)
        {
            var parts = id.Split('.');
            if (parts.Length != 2) return false;
            if (!Validators.IsValidId(parts[0])) return false;
            if (!Validators.IsGuid(parts[1])) return false;
            return true;
        }

        public static bool IsValidPreKeyId(string id)
        {
            return id.IsInteger() && int.Parse(id) > 0;
        }

        public static bool IsValidKeyId(string id, params string[] allowedTypes)
        {
            var parts = id.Split('.');
            if (parts.Length < 2 || parts.Length > 4) return false;
            var type = parts[0];
            if (!typeof(KeyType).GetConstants().ContainsKey(type)) return false;
            var keyId = string.Join(".", parts.Skip(1));
            if (!allowedTypes.IsNullOrEmpty())
            {
                if (!type.In(allowedTypes)) return false;
            }
            switch(type)
            {
                case KeyType.MasterKey:
                case KeyType.PrivateIdentityKey:
                case KeyType.PublicIdentityKey:
                case KeyType.PublishedIdentityKey:
                    return Validators.IsValidId(keyId);
                case KeyType.PrivateOneTimePreKey:
                case KeyType.PublicOneTimePreKey:
                case KeyType.PublishedOneTimePreKey:
                case KeyType.PrivateSignedPreKey:
                case KeyType.PublicSignedPreKey:
                case KeyType.PublishedSignedPreKey:
                    return IsValidPreKeyId(keyId);
                case KeyType.SenderKey:
                    return IsValidSenderKeyId(keyId);
                case KeyType.SecretKey:
                    return IsValidSecretKeyId(keyId);
            }
            return false;
        }
    }
}

