using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Globeport.Shared.Library.Extensions;

namespace Globeport.Shared.Library.Data
{
    public class StringTable : Dictionary<string,string>
    {
        public StringTable()
        {
        }

        public StringTable(IDictionary<string, string> strings)
            : base(strings)
        {
        }

        public new string this[string key]
        {
            get
            {
                return this.GetValue(key);
            }
            set
            {
                base[key] = value;
            }
        }
    }
}
