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
    public class PutModelValidator: AbstractValidator<PutModel>
    {
        MediaUploadValidator ImageUploadValidator { get; } = new MediaUploadValidator();
        public PutModelValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(i => i.Color).NotNull().Must(Validators.IsValidColor);
            RuleFor(i => i.ModelId).NotNull().Must(Validators.IsValidId);
            RuleFor(i => i.ImageUpload).SetValidator(ImageUploadValidator).When(i => i.ImageUpload != null);
            RuleFor(i => i.Label).NotNull().Must(Validators.IsValidShortDescription);
            RuleFor(i => i.Name).NotNull().Must(Validators.IsValidShortName);
        }
    }
}
