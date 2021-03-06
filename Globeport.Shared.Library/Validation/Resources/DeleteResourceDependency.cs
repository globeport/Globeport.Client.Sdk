﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization;

using FluentValidation;

using Globeport.Shared.Library.ApiModel;

namespace Globeport.Shared.Library.Validation
{
    public class DeleteResourceDependencyValidator: AbstractValidator<DeleteResourceDependency>
    {
        public DeleteResourceDependencyValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(i => i.ResourceId).NotNull().Must(Validators.IsValidId);
            RuleFor(i => i.DependencyId).NotNull().Must(Validators.IsValidId);
            RuleFor(i => i).Must(i => i.ResourceId != i.DependencyId);
        }
    }
}
