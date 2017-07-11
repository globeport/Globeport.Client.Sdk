using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Globeport.Shared.Library.Data;

namespace Globeport.Shared.Library.ClientModel
{
    public class Feedback : ClientObject
    {
        public string AccountId { get; set; }
        public string AppName { get; set; }
        public string Type { get; set; }
        public string Status { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Response { get; set; }
        public long Votes { get; set; }
        public bool Voted { get; set; }
        public DateTimeOffset Created { get; set; }
        public DateTimeOffset Updated { get; set; }
        public DateTimeOffset Timestamp { get; set; }


        public Feedback()
        {
        }

        public Feedback(string id, string accountId, string appName, string type, string status, string title, string description, string response, long votes, bool voted, DateTimeOffset created, DateTimeOffset updated, DateTimeOffset timestamp)
            : base(id)
        {
            AccountId = accountId;
            AppName = appName;
            Type = type;
            Status = status;
            Title = title;
            Description = description;
            Response = response;
            Votes = votes;
            Voted = voted;
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
