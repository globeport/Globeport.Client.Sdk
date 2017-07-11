using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

using Globeport.Shared.Library.Interfaces;
using Globeport.Shared.Library.Attributes;
using Globeport.Shared.Library.Components;

namespace Globeport.Shared.Library.ClientModel
{
    public class Interaction : ClientObject, IInteraction
    {
        public string EntityId { get; set; }
        public string AccountId { get; set; }
        public string Type { get; set; }
        public string Tag { get; set; }
        public byte[] Data { get; set; }
        [Ignore]
        public DataObject Properties { get; set; }
        public byte[] Signature { get; set; }
        public string PacketId { get; set; }
        public byte[] PacketKey { get; set; }
        public bool IsPublic { get; set; }
        [Ignore]
        public Avatar Avatar { get; set; }
        public DateTimeOffset Created { get; set; }
        public DateTimeOffset Updated { get; set; }
        public DateTimeOffset Timestamp { get; set; }

        public Interaction()
        {
        }

        public Interaction(string id, string entityId, string accountId, string type, string tag, byte[] data, byte[] signature, string packetId, byte[] packetKey, bool isPublic, DateTimeOffset created, DateTimeOffset updated, DateTimeOffset timestamp)
            : base(id)
        {
            EntityId = entityId;
            AccountId = accountId;
            Type = type;
            Tag = tag;
            Data = data;
            Signature = signature;
            PacketId = packetId;
            PacketKey = packetKey;
            IsPublic = isPublic;
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
