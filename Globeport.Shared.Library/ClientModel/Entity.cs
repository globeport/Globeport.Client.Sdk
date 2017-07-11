using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.IO;
using System.Threading.Tasks;

using Globeport.Shared.Library.Data;
using Globeport.Shared.Library.Extensions;
using Globeport.Shared.Library.Interfaces;
using Globeport.Shared.Library.Attributes;
using Globeport.Shared.Library.Components;

namespace Globeport.Shared.Library.ClientModel
{
    public class Entity : ClientObject, IEntity
    {
        public string AccountId { get; set; }
        public string ModelId { get; set; }
        public string ClassId { get; set; }
        public string ClassName { get; set; }
        public DataObject Data { get; set; }
        public Metadata Metadata { get; set; }
        public List<Media> Media { get; set; }
        public string KeyId { get; set; }
        public byte[] Key { get; set; }
        public string PacketId { get; set; }
        public byte[] PacketKey { get; set; }
        public byte[] Signature { get; set; }
        public bool IsPublic { get; set; }
        public bool IsHidden { get; set; }
        public string PortalId { get; set; }
        public DateTimeOffset Created { get; set; }
        public DateTimeOffset Updated { get; set; }
        public DateTimeOffset Timestamp { get; set; }

        [Ignore]
        [IgnoreDataMember]
        public List<Entity> Children { get; set; }
        [Ignore]
        public Model Model { get; set; }
        [Ignore]
        public Class Class { get; set; }
        [Ignore]
        public Avatar Avatar { get; set; }

        public Entity()
        {
        }

        //client entity constructor
        public Entity(string accountId, string classId, string modelId)
        {
            AccountId = accountId;
            ClassId = classId;
            ModelId = modelId;
            Data = new DataObject();
            Media = new List<Media>();
            Created = DateTimeOffset.UtcNow;
            Updated = DateTimeOffset.UtcNow;
            Timestamp = DateTimeOffset.UtcNow;
        }

        //server -> client contructor
        public Entity(string id, string accountId, string modelId, string classId, string className, string data, string metadata, List<Media> media, string keyId, byte[] key, string packetId, byte[] packetKey, byte[] signature, bool isPublic, bool isHidden, string portalId, DateTimeOffset created, DateTimeOffset updated, DateTimeOffset timestamp) 
            : base(id)
        {
            AccountId = accountId;
            ModelId = modelId;
            ClassId = classId;
            ClassName = className;
            Data = data.Deserialize<DataObject>();
            Metadata = metadata.Deserialize<Metadata>();
            Media = media;
            Signature = signature;
            KeyId = keyId;
            Key = key;
            PacketId = packetId;
            PacketKey = packetKey;
            IsPublic = isPublic;
            IsHidden = isHidden;
            PortalId = portalId;
            Created = created;
            Updated = updated;
            Timestamp = timestamp;
        }

        public override DateTimeOffset GetTimestamp()
        {
            return Timestamp;
        }

        public Avatar GetAvatar(string username)
        {
            var name = (string)Data["Name"];
            var biography = (string)Data["Biography"];
            var image = Media.FirstOrDefault(i=>i.Id == (string)Data["Image"])?.FileId ?? SystemImages.Avatar;
            var color = (string)Data["Color"];
            return new Avatar(Id, AccountId, username, name, biography, image, color, false, null, Created, Updated, Timestamp);
        }

        public static string GetAvatarData(string name, string color)
        {
            return new Dictionary<string, string>
            {
                { "Name", name },
                { "Biography", string.Empty },
                { "Image", SystemImages.Avatar },
                { "Type", "Personal" },
                { "Color", color }
            }.Serialize();
        }

        public bool IsAvatar()
        {
            return Model.IsAvatar(ModelId);
        }

        public bool IsPublicAvatar()
        {
            return Id == AccountId;
        }

        public bool IsPost()
        {
            return ClassId == SystemClasses.Post;
        }
    }
}
