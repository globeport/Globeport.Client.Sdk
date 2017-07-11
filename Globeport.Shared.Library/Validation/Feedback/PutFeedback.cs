using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization;

using FluentValidation;

using Globeport.Shared.Library.ApiModel;
using Globeport.Shared.Library.Data;
using Globeport.Shared.Library.Extensions;

namespace Globeport.Shared.Library.Validation
{
    public class PutFeedbackValidator: AbstractValidator<PutFeedback>
    {
        public PutFeedbackValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(i => i.FeedbackId).NotNull().Must(Validators.IsValidId);
            RuleFor(i => i.Label).NotNull().Must(Validators.IsValidShortDescription);
            RuleFor(i => i.Description).NotNull().Must(Validators.IsValidLongDescription);
            RuleFor(i => i.Status).Must(i=>typeof(FeedbackState).GetConstants().ContainsKey(i)).When(i => i.Status != null);
            RuleFor(i => i.Response).Must(Validators.IsValidLongDescription).When(i => i.Response != null);
        }
    }
}
