using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization;

using FluentValidation;

using Globeport.Shared.Library.Data;
using Globeport.Shared.Library.Extensions;
using Globeport.Shared.Library.ApiModel;

namespace Globeport.Shared.Library.Validation
{
    public class PostPortalValidator: AbstractValidator<PostPortal>
    {
        MediaUploadValidator ImageUploadValidator { get; } = new MediaUploadValidator();

        public PostPortalValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(i => i.Color).NotNull().Must(Validators.IsValidColor);
            RuleFor(i => i.Description).NotNull().Must(Validators.IsValidShortDescription);
            RuleFor(i => i.ImageUpload).NotNull().SetValidator(ImageUploadValidator);
            RuleFor(i => i.Name).NotNull().Must(Validators.IsValidShortName);
            RuleFor(i => i.Type).NotNull().Must(i => typeof(PortalType).GetConstants().ContainsKey(i) && i.In(PortalType.Group, PortalType.List));
        }
    }
}
