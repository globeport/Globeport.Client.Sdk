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
    public class PutPortalValidator: AbstractValidator<PutPortal>
    {
        MediaUploadValidator ImageUploadValidator { get; } = new MediaUploadValidator();

        public PutPortalValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(i => i.PortalId).NotNull().Must(Validators.IsValidId);
            RuleFor(i => i.Name).NotNull().Must(Validators.IsValidShortName);
            RuleFor(i => i.Description).NotNull().Must(Validators.IsValidShortDescription);
            RuleFor(i => i.Color).NotNull().Must(Validators.IsValidColor);
            RuleFor(i => i.ImageUpload).SetValidator(ImageUploadValidator).When(i => i.ImageUpload != null);
        }
    }
}
