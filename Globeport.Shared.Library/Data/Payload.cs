using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Globeport.Shared.Library.Data
{
    public class Payload
    {
        public string Id { get; set; }
        public byte[] Content { get; set; }

        public Payload()
        {
        }

        public Payload(string id, byte[] content)
        {
            Id = id;
            Content = content;
        }
    }
}
