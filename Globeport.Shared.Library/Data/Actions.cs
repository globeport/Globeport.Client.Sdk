using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Globeport.Shared.Library.Interfaces;

namespace Globeport.Shared.Library.Data
{
    public class Actions : Dictionary<string, string>
    {
        public Actions()
        {
        }

        public Actions(IDictionary<string, string> actions)
            : base(actions)
        {
        }
    }
}
