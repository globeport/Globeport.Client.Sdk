using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using FluentValidation;

using Globeport.Shared.Library.ApiModel;

namespace Globeport.Shared.Library.Validation
{
    public class HideEntityValidator: AbstractValidator<HideEntity>
    {
        public HideEntityValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(i => i.EntityId).NotNull().Must(Validators.IsValidId);
        }
    }
}
