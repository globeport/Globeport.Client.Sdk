using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Globeport.Shared.Library.ClientModel
{
    public class Activity : ClientObject
    {
        public string Type { get; set; }
        public string ContactId { get; set; }
        public string Data { get; set; }
        public DateTimeOffset Timestamp { get; set; }

        public Activity()
        {
        }

        public Activity(string id, string type, string contactId, string data, DateTimeOffset timestamp)
            : base(id)
        {
            Type = type;
            ContactId = contactId;
            Data = data;
            Timestamp = timestamp;
        }

        public override DateTimeOffset GetTimestamp()
        {
            return Timestamp;
        }
    }
}
