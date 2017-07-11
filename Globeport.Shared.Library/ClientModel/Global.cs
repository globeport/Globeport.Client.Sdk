using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Globeport.Shared.Library.ClientModel
{
    public class Global : ClientObject
    {
        public string Value { get; set; }

        public Global()
        {
        }

        public Global(string id, string value) 
            : base(id)
        {
            Value = value;
        }
    }
}
