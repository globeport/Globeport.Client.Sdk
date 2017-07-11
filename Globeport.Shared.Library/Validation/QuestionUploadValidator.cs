using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FluentValidation;

using Globeport.Shared.Library.Data;

namespace Globeport.Shared.Library.Validation
{
    public class QuestionUploadValidator: AbstractValidator<QuestionUpload>
    {
        public QuestionUploadValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(i => i.ModelId).NotNull().Must(Validators.IsValidId);
            RuleFor(i => i.Label).NotNull().Must(Validators.IsValidShortDescription);
            RuleFor(i => i.Description).Must(Validators.IsValidShortDescription).When(i => i.Description != null);
        }
    }
}
