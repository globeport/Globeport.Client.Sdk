using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using FluentValidation;

using Globeport.Shared.Library.ApiModel;

namespace Globeport.Shared.Library.Validation
{
    public class DeletePortalValidator: AbstractValidator<DeletePortal>
    {
        const int MaxPortalCount = 10;

        public DeletePortalValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(i => i.Portal).NotNull().Must(Validators.IsValidId);
        }
    }
}
