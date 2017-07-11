using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using FluentValidation;

using Globeport.Shared.Library.ApiModel;

namespace Globeport.Shared.Library.Validation
{
    public class GetAccountValidator: AbstractValidator<GetAccount>
    {
        public GetAccountValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;
        }
    }
}
