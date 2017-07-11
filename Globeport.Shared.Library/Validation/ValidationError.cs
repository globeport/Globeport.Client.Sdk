using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Globeport.Shared.Library.Extensions;

namespace Globeport.Shared.Library.Validation
{
    public class ValidationError
    {
        public string PropertyName { get; set; }
        public string ErrorMessage { get; set; }
        public string AttemptedValue { get; set; }

        public ValidationError()
        {
        }

        public ValidationError(string propertyName, string errorMessage)
        {
            PropertyName = propertyName;
            ErrorMessage = errorMessage;

        }
        public ValidationError(string propertyName, string errorMessage, object attemptedValue)
        {
            PropertyName = propertyName;
            ErrorMessage = errorMessage;
            if (attemptedValue is byte[])
            {
                AttemptedValue = $"byte[{((byte[])attemptedValue).Length}]";
            }
            else
            {
                AttemptedValue = attemptedValue.Serialize();
            }
        }
    }
}
