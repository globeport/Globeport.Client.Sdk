using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using FluentValidation;

using Globeport.Shared.Library.ApiModel;

namespace Globeport.Shared.Library.Validation
{
    public class PutInteractionValidator : AbstractValidator<PutInteraction>
    { 
        public PutInteractionValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(i => i.InteractionId).NotNull().Must(Validators.IsValidId);
            RuleFor(i => i.Tag).Must(Validators.IsValidShortName).When(i => i.Tag != null);
            RuleFor(i => i.Data).Must(Validators.IsValidInteractionData);
            RuleFor(i => i.Signature).NotNull().Must(Validators.IsValidTimestampedSignature);
        }
    }
}
