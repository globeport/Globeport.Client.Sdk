using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization;

using FluentValidation;

using Globeport.Shared.Library.ApiModel;

namespace Globeport.Shared.Library.Validation
{
    public class PostInteractionValidator : AbstractValidator<PostInteraction>
    {
        public PostInteractionValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(i => i.EntityId).NotNull().Must(Validators.IsValidId);
            RuleFor(i => i.Tag).Must(Validators.IsValidShortName).When(i => i.Tag != null);
            RuleFor(i => i.Type).NotNull().Must(Validators.IsValidShortName);
            RuleFor(i => i.Data).Must(Validators.IsValidInteractionData);
            RuleFor(i => i.Signature).NotNull().Must(Validators.IsValidTimestampedSignature);
        }
    }
}
