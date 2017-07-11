using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using FluentValidation;

using Globeport.Shared.Library.Data;
using Globeport.Shared.Library.ApiModel;

namespace Globeport.Shared.Library.Validation
{
    public class ResetCounterValidator: AbstractValidator<ResetCounter>
    {
        public ResetCounterValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(i => i.Id).Must(i=>i == Counters.Notifications);
        }
    }
}
