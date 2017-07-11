using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;

namespace Globeport.Shared.Library.Validation.Schema
{
    public class ImageValidator : JsonValidator
    {
        public override bool CanValidate(JSchema schema)
        {
            return true;
        }

        public override void Validate(JToken value, JsonValidatorContext context)
        {
            if (context.Schema.Type == JSchemaType.String && context.Schema.Format == "image")
            {
                var id = value.ToString();

                if (!(Validators.IsGuid(id) || Validators.IsValidId(id)))
                {
                    context.RaiseError($"'{id}' is not a valid image id.", id);
                }
            }
        }
    }
}
