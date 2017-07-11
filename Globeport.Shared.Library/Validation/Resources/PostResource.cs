using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using FluentValidation;

using Globeport.Shared.Library.ApiModel;
using Globeport.Shared.Library.Extensions;
using Globeport.Shared.Library.Data;

namespace Globeport.Shared.Library.Validation
{
    public class PostResourceValidator: AbstractValidator<PostResource>
    {
        public PostResourceValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(i => i.Label).NotNull().Must(Validators.IsValidLongDescription);
            RuleFor(i => i.Name).NotNull().Must(Validators.IsValidShortName);
            RuleFor(i => i.Type).NotNull().Must(i => typeof(ResourceTypes).GetConstants().ContainsKey(i));
        }
    }
}
