using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using FluentValidation;

using Globeport.Shared.Library.Data;
using Globeport.Shared.Library.ApiModel;

namespace Globeport.Shared.Library.Validation
{
    public class AuthenticateValidator : AbstractValidator<Authenticate>
    {
        public AuthenticateValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(i => i.Credentials).NotNull().Must(Validators.IsValidCredentials);
            RuleFor(i => i.Evidence).NotNull().Must(Validators.IsValidEvidence);
            RuleFor(i => i.Culture).NotNull().Must(Validators.IsValidCulture);
            RuleFor(i => i.Platform).Must(i=>i == Platforms.Windows);
            RuleFor(i => i.HardwareId).NotNull().Must(Validators.IsValidHardwareId);
        }
    }
}
