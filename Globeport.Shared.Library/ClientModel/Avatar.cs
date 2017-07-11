using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Globeport.Shared.Library.Attributes;
using Globeport.Shared.Library.Interfaces;

namespace Globeport.Shared.Library.ClientModel
{
    public class Avatar : ClientObject, IAvatar
    {
        public string AccountId { get; set; }
        public string Username { get; set; }
        public string Name { get; set; }
        public string Biography { get; set; }
        public string ImageId { get; set; }
        public string Color { get; set; }
        public bool IsContact { get; set; }
        [Ignore]
        public bool? IsMember { get; set; }
        public DateTimeOffset Created { get; set; }
        public DateTimeOffset Updated { get; set; }
        public DateTimeOffset Timestamp { get; set; }

        public Avatar()
        {
        }

        public Avatar(string id, string accountId, string username, string name, string biography, string imageId, string color, bool isContact, bool? isMember, DateTimeOffset created, DateTimeOffset updated, DateTimeOffset timestamp)
            : base(id)
        {
            AccountId = accountId;
            Username = username;
            Name = name;
            Biography = biography;
            ImageId = imageId;
            Color = color;
            IsContact = isContact;
            IsMember = isMember;
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
