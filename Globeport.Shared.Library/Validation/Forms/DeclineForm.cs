using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using FluentValidation;

using Globeport.Shared.Library.ApiModel;

namespace Globeport.Shared.Library.Validation
{
    public class DeclineFormValidator: AbstractValidator<DeclineForm>
    {
        public DeclineFormValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(i => i.FormId).NotNull().Must(Validators.IsValidId);
        }
    }
}
