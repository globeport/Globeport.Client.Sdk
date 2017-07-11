using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Threading.Tasks;

using Globeport.Shared.Library.Data;
using Globeport.Shared.Library.Attributes;

namespace Globeport.Shared.Library.ClientModel
{
    public class Portal : ClientObject
    {
        public string Type { get; set; }
        public int Index { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageId { get; set; }
        public string Color { get; set; }
        public GroupInfo GroupInfo { get; set; }
        public string Status { get; set; }
        [Ignore]
        public bool? IsSelected { get; set; }
        public DateTimeOffset Created { get; set; }
        public DateTimeOffset Updated { get; set; }
        public DateTimeOffset Timestamp { get; set; }

        public Portal()
        {
        }

        public Portal(Contact contact) 
            : this(contact.Id, PortalType.Contact, contact.Name, contact.Biography, contact.ImageId, contact.Color, null, contact.IsConnected ? PortalStates.Connected : PortalStates.Disconnected, null, contact.Created, contact.Updated, contact.Timestamp)
        {
        }

        public Portal(string type, string color)
        {
            Type = type;
            Color = color;
            Description = string.Empty;
            Created = DateTimeOffset.UtcNow;
            Updated = DateTimeOffset.UtcNow;
            Timestamp = DateTimeOffset.UtcNow;
        }

        public Portal(string id, string type, string name, string description, string imageId, string color, GroupInfo groupInfo, string status, bool? isSelected, DateTimeOffset created, DateTimeOffset updated, DateTimeOffset timestamp)
            : base(id)
        {
            Type = type;
            Name = name;
            Index = PortalType.GetIndex(type);
            Description = description;
            ImageId = imageId;
            Color = color;
            GroupInfo = groupInfo;
            Status = status;
            IsSelected = isSelected;
            Created = created;
            Updated = updated;
            Timestamp = timestamp;
        }

        public void UpdateAvatar(Avatar avatar)
        {
            Name = avatar.Name;
            Description = avatar.Biography;
            ImageId = avatar.ImageId;
            Color = avatar.Color;
            Timestamp = avatar.Timestamp;
        }

        public override DateTimeOffset GetTimestamp()
        {
            return Timestamp;
        }

        public bool IsGroup()
        {
            return Type == PortalType.Group;
        }

        public bool IsContact()
        {
            return Type == PortalType.Contact;
        }

        public bool IsProfile()
        {
            return Type == PortalType.Profile;
        }

        public bool IsList()
        {
            return Type == PortalType.List;
        }

        public bool IsConnected()
        {
            return Status == PortalStates.Connected;
        }

        public bool IsDisconnected()
        {
            return Status == PortalStates.Disconnected;
        }
    }
}
