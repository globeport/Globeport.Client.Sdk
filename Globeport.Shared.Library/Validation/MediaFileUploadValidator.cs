using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FluentValidation;

using Globeport.Shared.Library.Data;

namespace Globeport.Shared.Library.Validation
{
    public class MediaFileUploadValidator: AbstractValidator<MediaFileUpload>
    {
        string Type { get; }
        public MediaFileUploadValidator(string type)
        {
            Type = type;
            RuleFor(i => i.Size).Must(i=>type != MediaTypes.Image || ImageSizes.All.Contains(i));
            RuleFor(i => i.Data).NotNull().Must(IsValidData);
            RuleFor(i => i.Signature).NotNull().Must(Validators.IsValidTimestampedSignature);
        }

        bool IsValidData(MediaFileUpload file, byte[] data)
        {
            switch (Type)
            {
                case MediaTypes.Image:
                    //assume max 9 bits per pixel
                    switch (file.Size)
                    {
                        case 32:
                            if (data.Length > 131072) return false;
                            break;
                        case 64:
                            if (data.Length > 131072) return false;
                            break;
                        case 128:
                            if (data.Length > 131072) return false;
                            break;
                        case 256:
                            if (data.Length > 262144) return false;
                            break;
                        case 512:
                            if (data.Length > 524288) return false;
                            break;
                        case 1024:
                            if (data.Length > 1048576) return false;
                            break;

                    }
                    break;
                case MediaTypes.Ink:
                    if (data.Length > 131072) return false;
                    break;
            }
            return true;
        }
    }
}
