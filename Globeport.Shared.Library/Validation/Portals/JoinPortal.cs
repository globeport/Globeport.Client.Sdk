using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FluentValidation;

using Globeport.Shared.Library.ApiModel;

namespace Globeport.Shared.Library.Validation
{
    public class JoinPortalValidator: AbstractValidator<JoinPortal>
    {
        public JoinPortalValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(i => i.PortalId).NotNull().Must(Validators.IsValidId);
        }
    }
}
