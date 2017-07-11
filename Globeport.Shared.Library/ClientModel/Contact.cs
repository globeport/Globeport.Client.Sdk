using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

using Globeport.Shared.Library.Attributes;
using Globeport.Shared.Library.Interfaces;

namespace Globeport.Shared.Library.ClientModel
{
    public class Contact : ClientObject, IContact
    {
        public string Username { get; set; }
        public string Name { get; set; }
        public string Biography { get; set; }
        public string Color { get; set; }
        public string ImageId { get; set; }
        public string AliceAvatarId { get; set; }
        public string BobAvatarId { get; set; }
        public bool IsConnected { get; set; }
        [Ignore]
        public bool? IsSelected { get; set; }
        public DateTimeOffset Created { get; set; }
        public DateTimeOffset Updated { get; set; }
        public DateTimeOffset Timestamp { get; set; }

        public Contact()
        {
        }

        public Contact(string id, string username, string name, string biography, string color, string imageId, string aliceAvatarId, string bobAvatarId, bool isConnected, bool? isSelected, DateTimeOffset created, DateTimeOffset updated, DateTimeOffset timestamp)
            : base(id)
        {
            Username = username;
            Name = name;
            Biography = biography;
            Color = color;
            ImageId = imageId;
            AliceAvatarId = aliceAvatarId;
            BobAvatarId = bobAvatarId;
            IsConnected = isConnected;
            IsSelected = isSelected;
            Created = created;
            Updated = updated;
            Timestamp = timestamp;
        }

        public Avatar GetAvatar()
        {
            return new Avatar(Id, Id, Username, Name, Biography, ImageId, Color, IsConnected, null, Created, Updated, Timestamp);
        }

        public override DateTimeOffset GetTimestamp()
        {
            return Timestamp;
        }
    }
}
