using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FluentValidation;

using Globeport.Shared.Library.ApiModel;

namespace Globeport.Shared.Library.Validation
{
    public class ResumeValidator: AbstractValidator<Resume>
    {
        public ResumeValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;
        }
    }
}
