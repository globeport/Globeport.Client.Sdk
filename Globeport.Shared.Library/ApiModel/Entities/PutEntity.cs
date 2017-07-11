using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Globeport.Shared.Library.Components;
using Globeport.Shared.Library.ClientModel;
using Globeport.Shared.Library.Data;
using Globeport.Shared.Library.Extensions;
using Globeport.Shared.Library.Interfaces;

namespace Globeport.Shared.Library.ApiModel
{
    public class PutEntity : ApiRequest, IEntityRequest
    {
        public string EntityId { get; set; }
        public DataObject Data { get; set; }
        public byte[] Signature { get; set; }
        public string PacketId { get; set; }
        public List<SignalMessageUpload> Messages { get; set; }
        public List<MediaUpload> MediaUploads { get; set; }
        public Dictionary<string, byte[]> Keys { get; set; }
        public List<string> AddPortals { get; set; }
        public List<string> RemovePortals { get; set; }

        public PutEntity()
        {
        }

        public PutEntity(string entityId, DataObject data, byte[] signature, string packetId, IEnumerable<SignalMessageUpload> messages, IEnumerable<MediaUpload> mediaUploads, Dictionary<string, byte[]> keys, IEnumerable<string> addPortals, IEnumerable<string> removePortals)
        {
            EntityId = entityId;
            Data = data;
            Signature = signature;
            PacketId = packetId;
            Messages = messages.ToList();
            MediaUploads = mediaUploads?.ToList();
            Keys = keys;
            AddPortals = addPortals?.ToList();
            RemovePortals = removePortals?.ToList();
        }

        public override string GetPath()
        {
            return $"entities/{EntityId}";
        }

        public override string GetLogContent()
        {
            return new { EntityId = EntityId, Data = Data, AddPortals = AddPortals, RemovePortals = RemovePortals }.Serialize();
        }
    }

    public class PutEntityResponse : ApiResponse
    {
        public Entity Entity { get; set; }
        public List<Portal> MissingPortals { get; set; }
        public List<string> ExtraPortals { get; set; }

        public PutEntityResponse()  { }

        public PutEntityResponse(Entity entity)
        {
            Entity = entity;
        }
    }
}
