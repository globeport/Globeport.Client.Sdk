using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Globeport.Shared.Library.Xaml
{
    public class AttachedProperties : Dictionary<string, object>
    {
        public AttachedProperties()
        {
        }

        public AttachedProperties(IDictionary<string, object> properties)
            : base(properties)
        {
        }
    }
}
