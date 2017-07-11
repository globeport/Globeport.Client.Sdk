using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization;

using FluentValidation;

using Globeport.Shared.Library.ApiModel;
using Globeport.Shared.Library.Data;
using Globeport.Shared.Library.Extensions;

namespace Globeport.Shared.Library.Validation
{
    public class PostKeysValidator: AbstractValidator<PostKeys>
    {
        KeyUploadValidator KeyValidator { get; } = new KeyUploadValidator(false);

        public PostKeysValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(i => i.Keys).NotNull().Must(AreValidKeys).SetCollectionValidator(KeyValidator);
        }

        bool AreValidKeys(List<KeyUpload> keys)
        {
            if (keys.Any(i=> i.IsOneTimePreKey()))
            {
                if (keys.Count != 300) return false;
                if (keys.Any(i => !i.IsOneTimePreKey())) return false;
                if (keys.Count(i => i.Type == KeyType.PublishedOneTimePreKey) != 100) return false;
                if (keys.Count(i => i.Type == KeyType.PublicOneTimePreKey) != 100) return false;
                if (keys.Count(i => i.Type == KeyType.PrivateOneTimePreKey) != 100) return false;
                if (keys.Where(i => i.Type == KeyType.PublishedOneTimePreKey).Distinct(i => i.KeyId).Count() != keys.Where(i => i.Type == KeyType.PublishedOneTimePreKey).Count()) return false;
                if (keys.Where(i => i.Type == KeyType.PublicOneTimePreKey).Distinct(i => i.KeyId).Count() != keys.Where(i => i.Type == KeyType.PublishedOneTimePreKey).Count()) return false;
                if (keys.Where(i => i.Type == KeyType.PrivateOneTimePreKey).Distinct(i => i.KeyId).Count() != keys.Where(i => i.Type == KeyType.PublishedOneTimePreKey).Count()) return false;
                if (keys.Where(i => i.Type == KeyType.PublishedOneTimePreKey && !KeyValidator.OneTimePreKeyIds.Contains(i.KeyId)).Any()) return false;
                if (keys.Where(i => i.Type == KeyType.PublishedOneTimePreKey && !KeyValidator.OneTimePreKeyIds.Contains(i.KeyId)).Any()) return false;
                if (keys.Where(i => i.Type == KeyType.PublicOneTimePreKey && !KeyValidator.OneTimePreKeyIds.Contains(i.KeyId)).Any()) return false;
                return true;
            }
            else
            {
                if (keys.Any(i => !i.Type.In(KeyType.PublicIdentityKey, KeyType.SenderKey, KeyType.SecretKey))) return false;
                if (keys.Where(i => i.Type == KeyType.PublicIdentityKey).Distinct(i => i.KeyId).Count() != keys.Where(i => i.Type == KeyType.PublicIdentityKey).Count()) return false;
                if (keys.Where(i => i.Type == KeyType.SenderKey).Distinct(i => i.KeyId).Count() != keys.Where(i => i.Type == KeyType.SenderKey).Count()) return false;
                if (keys.Where(i => i.Type == KeyType.SecretKey).Distinct(i => i.KeyId).Count() != keys.Where(i => i.Type == KeyType.SecretKey).Count()) return false;
                return true;
            }
        }
    }
}
