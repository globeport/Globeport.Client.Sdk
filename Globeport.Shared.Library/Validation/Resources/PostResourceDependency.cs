using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using FluentValidation;

using Globeport.Shared.Library.ApiModel;

namespace Globeport.Shared.Library.Validation
{
    public class PostResourceDependencyValidator: AbstractValidator<PostResourceDependency>
    {
        public PostResourceDependencyValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(i => i.ResourceId).NotNull().Must(Validators.IsValidId);
            RuleFor(i => i.DependencyId).NotNull().Must(Validators.IsValidId);
            RuleFor(i => i).Must(i => i.ResourceId != i.DependencyId);
        }
    }
}
