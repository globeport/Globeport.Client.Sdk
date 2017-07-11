using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization;

using FluentValidation;

using Globeport.Shared.Library.ApiModel;
using Globeport.Shared.Library.Extensions;
using Globeport.Shared.Library.Data;

namespace Globeport.Shared.Library.Validation
{
    public class DeleteKeysValidator: AbstractValidator<DeleteKeys>
    {
        public string[] AllowedTypes = new[] { KeyType.PublicOneTimePreKey, KeyType.PrivateOneTimePreKey, KeyType.SenderKey, KeyType.SecretKey };

        public DeleteKeysValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(i => i.Keys).NotNull().Must(AreValidKeys);
        }

        bool AreValidKeys(List<string> keys)
        {
            if (keys.Count == 0) return false;
            if (keys.Count > Globals.MaxDeleteCount) return false;
            if (keys.Distinct().Count() != keys.Count) return false;
            if (keys.Any(i => !KeyUploadValidator.IsValidKeyId(i, AllowedTypes))) return false;
            return true;
        }
    }
}
