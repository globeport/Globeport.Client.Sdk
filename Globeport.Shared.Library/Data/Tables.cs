using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Globeport.Shared.Library.Interfaces;

namespace Globeport.Shared.Library.Data
{
    public class Tables : Dictionary<string,Table>
    {
        public Tables()
        {
        }

        public Tables(IDictionary<string, Table> tables)
            : base(tables)
        {
        }
    }
}
