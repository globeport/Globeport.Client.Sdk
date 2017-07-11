using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using FluentValidation;

using Globeport.Shared.Library.ApiModel;

namespace Globeport.Shared.Library.Validation
{
    public class PutResourceValidator: AbstractValidator<PutResource>
    {
        public PutResourceValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(i => i.ResourceId).NotNull().Must(Validators.IsValidId);
            RuleFor(i => i.Data).Must(Validators.IsValidResourceData).When(i => i.Data != null);
            RuleFor(i => i.Label).Must(Validators.IsValidLongDescription).When(i => i.Label != null);
            RuleFor(i => i.Name).Must(Validators.IsValidShortName).When(i => i.Name != null);
        }
    }
}
