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
    public class DeleteModelValidator: AbstractValidator<DeleteModel>
    {
        public DeleteModelValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(i => i.ModelId).NotNull().Must(Validators.IsValidId);
        }
    }
}
