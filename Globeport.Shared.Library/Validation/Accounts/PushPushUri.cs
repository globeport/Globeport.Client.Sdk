using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using FluentValidation;

using Globeport.Shared.Library.ApiModel;

namespace Globeport.Shared.Library.Validation
{
    public class PutPushUriValidator: AbstractValidator<PutPushUri>
    {
        public PutPushUriValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(i => i.PushUri).Must(Validators.IsValidAbsoluteUri).When(i => i.PushUri != null);
        }
    }
}
