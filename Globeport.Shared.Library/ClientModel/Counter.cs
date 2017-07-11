using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Globeport.Shared.Library.ClientModel
{
    public class Counter : ClientObject
    {
        public long Value { get; set; }

        public Counter()
        {
        }

        public Counter(string id, long value)
            : base(id)
        {
            Value = value;
        }
    }
}
