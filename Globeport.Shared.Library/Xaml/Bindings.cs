using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Globeport.Shared.Library.Xaml
{
    public class Bindings : Dictionary<string, Binding>
    {
        public Bindings()
        {
        }

        public Bindings(IDictionary<string, Binding> bindings)
            : base(bindings)
        {
        }
    }
}
