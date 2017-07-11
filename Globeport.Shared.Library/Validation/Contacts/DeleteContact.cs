using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using FluentValidation;

using Globeport.Shared.Library.ApiModel;

namespace Globeport.Shared.Library.Validation
{
    public class DeleteContactValidator: AbstractValidator<DeleteContact>
    {
        public DeleteContactValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(i => i.ContactId).NotNull().Must(Validators.IsValidId);
        }
    }
}
