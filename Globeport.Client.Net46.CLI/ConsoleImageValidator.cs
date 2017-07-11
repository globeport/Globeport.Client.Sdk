using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;

using Globeport.Shared.Library.Data;
using Globeport.Client.Net46.CLI.Commands;
using Globeport.Shared.Library.Validation;

namespace Globeport.Client.Net46.CLI
{
    public class ConsoleImageValidator : JsonValidator
    {
        public List<MediaUpload> Uploads { get; } = new List<MediaUpload>();
        public Dictionary<string, string> ImagePaths { get; } = new Dictionary<string, string>();

        public override bool CanValidate(JSchema schema)
        {
            return true;
        }

        public override void Validate(JToken token, JsonValidatorContext context)
        {
            if (context.Schema.Type == JSchemaType.String && context.Schema.Format == "image")
            {
                var path = token.ToString();

                if (!Validators.IsValidId(path))
                {
                    var upload = Command.GetImageUpload(path);

                    if (upload != null)
                    {
                        var aspectRatio = (double) context.Schema.ExtensionData["aspectRatio"];

                        if (upload.AspectRatio == aspectRatio)
                        {
                            upload.IsValid = true;
                            Uploads.Add(upload);
                            ImagePaths.Add(token.Path, upload.Id);
                        }
                        else
                        {
                            context.RaiseError($"Image upload '{path}' does not have the correct aspect ratio (expected {aspectRatio}).", path);
                        }
                    }
                    else
                    {
                        context.RaiseError($"'{path}' is not a valid image id.", path);
                    }
                }
            }
        }
    }
}
