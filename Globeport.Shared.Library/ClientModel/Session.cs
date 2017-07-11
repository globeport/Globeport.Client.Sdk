using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Globeport.Shared.Library.Data;

namespace Globeport.Shared.Library.ClientModel
{
    public class Session : ClientObject
    {
        public string Platform { get; set; }
        public DateTimeOffset Timestamp { get; set; }

        public Session()
        {
        }

        public Session(string id)
        {
            Id = id;
            Timestamp = DateTimeOffset.UtcNow;
        }

        public Session(string id, string platform, DateTimeOffset timestamp) 
            : base(id)
        {
            Platform = platform;
            Timestamp = timestamp;
        }

        public override DateTimeOffset GetTimestamp()
        {
            return Timestamp;
        }
    }
}
