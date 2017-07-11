using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FluentValidation;

using Globeport.Shared.Library.Data;

namespace Globeport.Shared.Library.Validation
{
    public class AnswerUploadValidator: AbstractValidator<AnswerUpload>
    {
        public AnswerUploadValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(i => i.EntityId).NotNull().Must(Validators.IsValidId);
            RuleFor(i => i.QuestionId).NotNull().Must(Validators.IsValidId);
        }
    }
}
