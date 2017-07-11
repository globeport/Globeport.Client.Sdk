using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

using Globeport.Shared.Library;
using Globeport.Shared.Library.ApiModel;

namespace Globeport.Shared.Library.Exceptions
{
    public class ApiException : Exception
    {
        public string Content { get; set; }

        public ApiException()
            : base(Resources.GetString("ApiError"))
        {
        }

        public ApiException(string message, string content = null)
            : base(message)
        {
            Content = content;
        }

        public ApiError ToApiError()
        {
            return new ApiError(this);
        }
    }
}
