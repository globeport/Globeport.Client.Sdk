using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization;

using FluentValidation;

using Globeport.Shared.Library.ApiModel;

namespace Globeport.Shared.Library.Validation
{
    public class GetKeyBundleValidator: AbstractValidator<GetKeyBundle>
    {
        public GetKeyBundleValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(i => i.AccountId).NotNull().Must(Validators.IsValidId);
        }
    }
}
