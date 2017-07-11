using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;

using Globeport.Shared.Library.Data;
using Globeport.Shared.Library.Extensions;

namespace Globeport.Shared.Library.Validation.Schema
{
    public class CellValidator : JsonValidator
    {
        public Tables Tables { get; }

        public CellValidator(Tables tables)
        {
            Tables = tables;
        }
        public override bool CanValidate(JSchema schema)
        {
            return schema.Format == "cell";
        }

        public override void Validate(JToken value, JsonValidatorContext context)
        {
            var cellValue = value.ToString();

            var table = Tables[(string)context.Schema.ExtensionData["table"]];

            if (!table.Rows.Any(i => i[(string)context.Schema.ExtensionData["column"]] == cellValue))
            {
                context.RaiseError($"'{cellValue}' is not a valid cell.", cellValue);
            }
        }
    }
}
