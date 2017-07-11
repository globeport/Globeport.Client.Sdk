using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;

namespace Globeport.Shared.Library.Validation.Schema
{
    public class ColorValidator : JsonValidator
    {
        public override bool CanValidate(JSchema schema)
        {
            return schema.Format == "color";
        }

        public override void Validate(JToken value, JsonValidatorContext context)
        {
            var color = value.ToString();

            if (!Validators.IsValidColor(color))
            {
                context.RaiseError($"'{color}' is not a valid color.", color);
            }
        }
    }
}
