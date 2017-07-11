using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using FluentValidation;

using Globeport.Shared.Library.ApiModel;

namespace Globeport.Shared.Library.Validation
{
    public class PostModelValidator: AbstractValidator<PostModel>
    {
        MediaUploadValidator ImageUploadValidator { get; } = new MediaUploadValidator();

        public PostModelValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(i => i.Name).NotNull().Must(Validators.IsValidShortName);
            RuleFor(i => i.Label).NotNull().Must(Validators.IsValidShortDescription);
            RuleFor(i => i.Color).NotNull().Must(Validators.IsValidColor);
            RuleFor(i => i.ImageUpload).NotNull().SetValidator(ImageUploadValidator);
        }
    }
}
