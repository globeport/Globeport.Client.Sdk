using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Globeport.Shared.Library.Data
{
    public class EntityPayload : Payload
    {
        public Dictionary<string, byte[]> Keys { get; set; }

        public EntityPayload()
        {
        }

        public EntityPayload(string id, byte[] content, Dictionary<string, byte[]> keys)
            : base(id, content)
        {
            Keys = keys;
        }
    }
}
 