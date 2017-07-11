using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Globeport.Shared.Library.ClientModel
{
    public class Form : ClientObject
    {
        public string ContactId { get; set; }
        public string Status { get; set; }
        public List<Question> Questions { get; set; }
        public string CreatorId { get; set; }
        public DateTimeOffset Created { get; set; }
        public DateTimeOffset Updated { get; set; }
        public DateTimeOffset Timestamp { get; set; }

        public Form()
        {
        }

        public Form(string id, string contactId, string status, List<Question> questions, string creatorId, DateTimeOffset created, DateTimeOffset updated, DateTimeOffset timestamp)
            : base(id)
        {
            ContactId = contactId;
            Status = status;
            Questions = questions;
            CreatorId = creatorId;
            Created = created;
            Updated = updated;
            Timestamp = timestamp;
        }

        public override DateTimeOffset GetTimestamp()
        {
            return Timestamp;
        }
    }
}
