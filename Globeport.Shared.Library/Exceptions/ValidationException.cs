using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

using Globeport.Shared.Library;
using Globeport.Shared.Library.Extensions;
using Globeport.Shared.Library.Validation;

namespace Globeport.Shared.Library.Exceptions
{
    public class ValidationException : ApiException
    {
        public ValidationException(IEnumerable<ValidationError> errors)
            : base(Resources.GetString("ValidationError"))
        {
            Content = errors.ToList().Serialize();
        }

        public ValidationException(string propertyName, string errorMessage)
            : base(Resources.GetString("ValidationError"))
        {
            Content = new List<ValidationError> { new ValidationError(propertyName, errorMessage) }.Serialize();
        }

        public ValidationException(string propertyName, string propertyValue, string errorMessage)
            : base(Resources.GetString("ValidationError"))
        {
            Content = new List<ValidationError> { new ValidationError(propertyName, propertyValue, errorMessage) }.Serialize();
        }

        public ValidationException(string content)
            : base(Resources.GetString("ValidationError"))
        {
            Content = content;
        }

        public List<ValidationError> GetErrors()
        {
            return Content.Deserialize<List<ValidationError>>();
        }
    }
}
