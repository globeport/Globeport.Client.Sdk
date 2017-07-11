using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Globeport.Shared.Library.ClientModel;

namespace Globeport.Shared.Library.Data
{
    public class CompositeKey : Dictionary<string, object>
    {
        public CompositeKey()
        {
        }
        public CompositeKey(Dictionary<string, object> compositeKey)
            : base(compositeKey)
        {
        }
    }
}
