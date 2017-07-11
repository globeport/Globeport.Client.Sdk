using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization;

using FluentValidation;

using Globeport.Shared.Library.Data;
using Globeport.Shared.Library.ApiModel;
using Globeport.Shared.Library.Extensions;

namespace Globeport.Shared.Library.Validation
{
    public class PostFeedbackValidator: AbstractValidator<PostFeedback>
    {
        public PostFeedbackValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(i => i.Type).NotNull().Must(i => typeof(FeedbackType).GetConstants().ContainsKey(i));
            RuleFor(i => i.AppName).NotNull().Must(i=>typeof(AppNames).GetConstants().ContainsKey(i));
            RuleFor(i => i.Label).NotNull().Must(Validators.IsValidShortDescription);
            RuleFor(i => i.Description).NotNull().Must(Validators.IsValidLongDescription);
        }
    }
}
