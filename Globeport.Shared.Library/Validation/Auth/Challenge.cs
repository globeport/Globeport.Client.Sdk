using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using FluentValidation;

using Globeport.Shared.Library.ApiModel;

namespace Globeport.Shared.Library.Validation
{
    public class ChallengeValidator: AbstractValidator<Challenge>
    {
        public ChallengeValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;
        }
    }
}
