using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization;

using FluentValidation;
using MoreLinq;

using Globeport.Shared.Library.ApiModel;
using Globeport.Shared.Library.Data;

namespace Globeport.Shared.Library.Validation
{
    public class SendFormValidator: AbstractValidator<SendForm>
    {
        QuestionUploadValidator QuestionValidator { get; } = new QuestionUploadValidator();

        public SendFormValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(i => i.ContactId).NotNull().Must(Validators.IsValidId);
            RuleFor(i => i.Questions).NotNull().Must(AreValidQuestions).SetCollectionValidator(QuestionValidator);
        }

        bool AreValidQuestions(List<QuestionUpload> questions)
        {
            if (questions.Count == 0) return false;
            if (questions.Count > Globals.MaxQuestionsCount) return false;
            return true;
        }
    }
}
