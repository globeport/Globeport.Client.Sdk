using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using FluentValidation;

using Globeport.Shared.Library.ApiModel;

namespace Globeport.Shared.Library.Validation
{
    public class DeleteInteractionValidator: AbstractValidator<DeleteInteraction>
    {
        public DeleteInteractionValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(i => i.InteractionId).NotNull().Must(Validators.IsValidId);
        }
    }
}
