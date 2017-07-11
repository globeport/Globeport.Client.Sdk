using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Globeport.Shared.Library.Data
{
    public class Headers : Dictionary<string, string>
    {
        public Headers()
        {
        }

        public Headers(IDictionary<string, string> header)
            : base(header)
        {
        }
    }
}
