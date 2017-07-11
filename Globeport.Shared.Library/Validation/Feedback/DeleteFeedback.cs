using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using FluentValidation;

using Globeport.Shared.Library.ApiModel;

namespace Globeport.Shared.Library.Validation
{
    public class DeleteFeedbackValidator : AbstractValidator<DeleteFeedback>
    {
        public DeleteFeedbackValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(i => i.FeedbackId).NotNull().Must(Validators.IsValidId);
        }
    }
}
