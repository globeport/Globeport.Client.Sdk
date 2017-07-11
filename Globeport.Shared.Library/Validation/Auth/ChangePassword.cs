using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

using FluentValidation;

using Globeport.Shared.Library.ApiModel;

namespace Globeport.Shared.Library.Validation
{
    public class ChangePasswordValidator: AbstractValidator<ChangePassword>
    {
        public ChangePasswordValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(i => i.Version).NotNull().Must(Validators.IsValidVersion);
            RuleFor(i => i.Credentials).NotNull().Must(Validators.IsValidCredentials);
            RuleFor(i => i.Evidence).NotNull().Must(Validators.IsValidEvidence);
            RuleFor(i => i.MasterKey).NotNull().Must(i=>i.Length == KeyUploadValidator.MasterKeyLength);
            RuleFor(i => i.Salt).NotNull().Must(Validators.IsValidSalt);
            RuleFor(i => i.Verifier).NotNull().Must(Validators.IsValidVerifier);
        }
    }
}
