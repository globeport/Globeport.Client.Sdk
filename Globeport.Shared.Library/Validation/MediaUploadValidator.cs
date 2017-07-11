using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FluentValidation;

using Globeport.Shared.Library.Data;
using Globeport.Shared.Library.Extensions;

namespace Globeport.Shared.Library.Validation
{
    public class MediaUploadValidator: AbstractValidator<MediaUpload>
    {
        MediaFileUploadValidator ImageFileValidator { get; } = new MediaFileUploadValidator(MediaTypes.Image);
        MediaFileUploadValidator InkFileValidator { get; } = new MediaFileUploadValidator(MediaTypes.Ink);

        public MediaUploadValidator()
        {
            RuleFor(i => i.Id).NotNull().Must(Validators.IsGuid);
            RuleFor(i => i.Type).NotNull().Must(i => typeof(MediaTypes).GetConstants().ContainsKey(i));
            RuleFor(i => i.Files).NotNull().Must(AreValidFiles).SetCollectionValidator(t=>GetMediaFileUploadValidator(t.Type));
            RuleFor(i => i.KeyId).NotNull().When(i => i.Key != null);
            RuleFor(i => i.KeyId).Must(Validators.IsGuid).When(i => i.KeyId != null);
            RuleFor(i => i.Key).Must(i => i.Length == KeyUploadValidator.SecretKeyLength).When(i => i.Key != null);
        }

        static bool AreValidFiles(MediaUpload upload, List<MediaFileUpload> files)
        {
            switch (upload.Type)
            {
                case MediaTypes.Image:
                    return files.Distinct().Count() == ImageSizes.All.Length;
                case MediaTypes.Ink:
                    return files.Distinct().Count() == 1;
                default:
                    return false;
            }
        }

        MediaFileUploadValidator GetMediaFileUploadValidator(string type)
        {
            switch(type)
            {
                case MediaTypes.Image:
                    return ImageFileValidator;
                case MediaTypes.Ink:
                    return InkFileValidator;
            }
            throw new NotSupportedException();
        }
    }
}
