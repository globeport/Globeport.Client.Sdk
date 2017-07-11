using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FluentValidation;
using MoreLinq;

using Globeport.Shared.Library.ApiModel;
using Globeport.Shared.Library.Data;

namespace Globeport.Shared.Library.Validation
{
    public class SignUpValidator: AbstractValidator<SignUp>
    {
        KeyUploadValidator KeyValidator { get; } = new KeyUploadValidator(true);

        public SignUpValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(i => i.Username).NotNull().Must(Validators.IsValidUsername);
            RuleFor(i => i.Color).NotNull().Must(Validators.IsValidColor);
            RuleFor(i => i.Signature).NotNull().Must(Validators.IsValidTimestampedSignature);
            RuleFor(i => i.Version).NotNull().Must(Validators.IsValidVersion);
            RuleFor(i => i.Salt).NotNull().Must(Validators.IsValidSalt);
            RuleFor(i => i.Verifier).NotNull().Must(Validators.IsValidVerifier);
            RuleFor(i => i.Keys).NotNull().Must(AreValidKeys).SetCollectionValidator(KeyValidator);
        }

        bool AreValidKeys(List<KeyUpload> keys)
        {
            if (keys.Count != 307) return false;
            if (keys.Count(i => i.Type == KeyType.MasterKey) != 1) return false;
            if (keys.Count(i => i.Type == KeyType.PublishedIdentityKey) != 1) return false;
            if (keys.Count(i => i.Type == KeyType.PublicIdentityKey) != 1) return false;
            if (keys.Count(i => i.Type == KeyType.PrivateIdentityKey) != 1) return false;
            if (keys.Count(i => i.Type == KeyType.PublishedSignedPreKey) != 1) return false;
            if (keys.Count(i => i.Type == KeyType.PublicSignedPreKey) != 1) return false;
            if (keys.Count(i => i.Type == KeyType.PrivateSignedPreKey) != 1) return false;
            if (keys.Count(i => i.Type == KeyType.PublishedOneTimePreKey) != 100) return false;
            if (keys.Count(i => i.Type == KeyType.PublicOneTimePreKey) != 100) return false;
            if (keys.Count(i => i.Type == KeyType.PrivateOneTimePreKey) != 100) return false;
            if (keys.Where(i => i.Type == KeyType.PublishedOneTimePreKey).DistinctBy(i => i.KeyId).Count() != keys.Where(i => i.Type == KeyType.PublishedOneTimePreKey).Count()) return false;
            if (keys.Where(i => i.Type == KeyType.PublicOneTimePreKey).DistinctBy(i => i.KeyId).Count() != keys.Where(i => i.Type == KeyType.PublishedOneTimePreKey).Count()) return false;
            if (keys.Where(i => i.Type == KeyType.PrivateOneTimePreKey).DistinctBy(i => i.KeyId).Count() != keys.Where(i => i.Type == KeyType.PublishedOneTimePreKey).Count()) return false;
            if (keys.Where(i => i.Type == KeyType.PublishedOneTimePreKey && !KeyValidator.OneTimePreKeyIds.Contains(i.KeyId)).Any()) return false;
            if (keys.Where(i => i.Type == KeyType.PublishedOneTimePreKey && !KeyValidator.OneTimePreKeyIds.Contains(i.KeyId)).Any()) return false;
            if (keys.Where(i => i.Type == KeyType.PublicOneTimePreKey && !KeyValidator.OneTimePreKeyIds.Contains(i.KeyId)).Any()) return false;
            if (keys.Single(i => i.Type == KeyType.PublishedSignedPreKey).KeyId != KeyValidator.SignedPreKeyId) return false;
            if (keys.Single(i => i.Type == KeyType.PublicSignedPreKey).KeyId != KeyValidator.SignedPreKeyId) return false;
            if (keys.Single(i => i.Type == KeyType.PrivateSignedPreKey).KeyId != KeyValidator.SignedPreKeyId) return false;
            return true;
        }
    }
}
