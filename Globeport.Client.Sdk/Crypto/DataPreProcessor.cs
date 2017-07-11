using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json.Schema;
using Newtonsoft.Json.Linq;

using Globeport.Shared.Library.Components;

namespace Globeport.Client.Sdk.Crypto
{
    public class DataPreProcessor
    {
        public JToken Token { get; private set; }
        public JSchema Schema { get; }

        public DataPreProcessor(DataObject data, JSchema schema)
        {
            Token = JToken.FromObject(data.RemoveNulls());
            Schema = schema;
        }

        public DataObject Process()
        {
            Token.DeepClone().Validate(Schema, OnValidationError);

            return Token.ToObject<DataObject>();
        }

        void OnValidationError(object sender, SchemaValidationEventArgs e)
        {
            if (e.ValidationError.ErrorType == ErrorType.AdditionalProperties)
            {
                var token = Token.SelectToken(e.Path);
                if (token != null)
                {
                    token.Parent.Remove();
                }
            }
        }
    }
}
