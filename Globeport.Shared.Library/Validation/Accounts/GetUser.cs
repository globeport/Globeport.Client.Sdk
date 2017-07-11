using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using FluentValidation;

using Globeport.Shared.Library.ApiModel;

namespace Globeport.Shared.Library.Validation
{
    public class GetUserValidator: AbstractValidator<GetUser>
    {
        public GetUserValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(i => i.Username).NotNull().Must(Validators.IsValidUsername);
        }
    }
}
