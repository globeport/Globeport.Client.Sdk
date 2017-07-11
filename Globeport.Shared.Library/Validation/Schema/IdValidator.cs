using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;

namespace Globeport.Shared.Library.Validation.Schema
{
    public class IdValidator : JsonValidator
    {
        public override bool CanValidate(JSchema schema)
        {
            return schema.Format == "id";
        }

        public override void Validate(JToken value, JsonValidatorContext context)
        {
            var id = value.ToString();

            if (!Validators.IsValidId(id))
            {
                context.RaiseError($"'{id}' is not a valid id.", id);
            }
        }
    }
}
