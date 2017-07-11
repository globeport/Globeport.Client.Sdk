using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Threading;

namespace Globeport.Shared.Library.ApiModel
{
    public class ApiResponse
    {
        public ApiError Error { get; set; }

        public ApiResponse()
        {
        }

        public void ThrowOnError()
        {
            if (Error != null) throw Error.ToException();
        }
    }
}
