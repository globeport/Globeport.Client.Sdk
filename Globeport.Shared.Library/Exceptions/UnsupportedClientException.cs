using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

using Globeport.Shared.Library;

namespace Globeport.Shared.Library.Exceptions
{
    public class UnsupportedClientException : ApiException
    {
        public UnsupportedClientException()
            : base(Resources.GetString("UnsupportedClient"))
        {
        }
    }
}
