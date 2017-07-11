using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FluentValidation;

using Globeport.Shared.Library.ApiModel;

namespace Globeport.Shared.Library.Validation
{
    public class LogInValidator: AbstractValidator<LogIn>
    {
        public LogInValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(i => i.Username).NotNull().Must(Validators.IsValidUsername);
            RuleFor(i => i.Version).NotNull().Must(Validators.IsValidVersion);
        }
    }
}
