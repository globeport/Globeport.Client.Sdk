using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;
using System.Runtime.Serialization;

using Globeport.Shared.Library.Interfaces;
using Globeport.Shared.Library.Data;

namespace Globeport.Shared.Library.ClientModel
{
    public class Account : ClientObject, IAccount, ISession
    {
        public string Username { get; set; }
        public string Name { get; set; }
        public string Biography { get; set; }
        public string ImageId { get; set; }
        public string Color { get; set; }
        public byte[] Salt { get; set; }
        public long DeviceId { get; set; }
        public string SessionId { get; set; }
        [IgnoreDataMember]
        public byte[] SessionKey { get; set; }
        [IgnoreDataMember]
        public bool IsAuthenticated { get; set; }
        [IgnoreDataMember]
        public bool IsOpen { get; set; }
        public string Version { get; set; }
        public List<string> Roles { get; set; }
        public Dictionary<string,string> Portals { get; set; }
        public DateTimeOffset Created { get; set; }
        public DateTimeOffset Updated { get; set; }
        public DateTimeOffset Timestamp { get; set; }

        public Account()
        {
        }

        public Account(string id, string username, string name, string biography, string imageId, string color, byte[] salt, string version, List<string> roles, Dictionary<string,string> portals, DateTimeOffset created, DateTimeOffset updated, DateTimeOffset timestamp) 
            :base(id)
        {
            Username = username;
            Name = name;
            Biography = biography;
            ImageId = imageId;
            Color = color;
            Salt = salt;
            Version = version;
            Roles = roles;
            Portals = portals;
            Created = created;
            Updated = updated;
            Timestamp = timestamp;
        }

        public Avatar GetAvatar()
        {
            return new Avatar(Id, Id, Username, Name, Biography, ImageId, Color, false, null, Created, Updated, Timestamp);
        }

        public void UpdateAvatar(Avatar avatar)
        {
            Username = avatar.Username;
            Name = avatar.Name;
            Biography = avatar.Biography;
            ImageId = avatar.ImageId;
            Color = avatar.Color;
            Timestamp = avatar.Timestamp;
        }

        public override DateTimeOffset GetTimestamp()
        {
            return Timestamp;
        }

        public bool IsAdministrator()
        {
            return Roles.Contains(SystemRoles.Administrator);
        }
    }
}
