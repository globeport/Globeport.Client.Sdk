using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using FluentValidation;

using Globeport.Shared.Library.ApiModel;

namespace Globeport.Shared.Library.Validation
{
    public class LogOutValidator: AbstractValidator<LogOut>
    {
        public LogOutValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(i => i.SessionId).NotNull().Must(Validators.IsGuid);
        }
    }
}
