using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

using FluentValidation;

using Globeport.Shared.Library.ApiModel;

namespace Globeport.Shared.Library.Validation
{
    public class GetSessionsValidator: AbstractValidator<GetSessions>
    {
        public GetSessionsValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;
        }
    }
}
