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
    public class PostEntity : ApiRequest, IEntityRequest
    {
        public string ModelId { get; set; }
        public DataObject Data { get; set; }
        public byte[] Signature { get; set; }
        public string KeyId { get; set; }
        public string PacketId { get; set; }
        public List<SignalMessageUpload> Messages { get; set; }
        public List<MediaUpload> MediaUploads { get; set; }
        public Dictionary<string, byte[]> Keys { get; set; }
        public List<string> Portals { get; set; }

        public PostEntity()
        {
        }

        public PostEntity(string modelId, DataObject data, byte[] signature, string keyId, string packetId, IEnumerable<SignalMessageUpload> messages, IEnumerable<MediaUpload> mediaUploads, Dictionary<string,byte[]> keys, IEnumerable<string> portals)
        {
            ModelId = modelId;
            Data = data;
            Signature = signature;
            KeyId = keyId;
            PacketId = packetId;
            Messages = messages.ToList();
            MediaUploads = mediaUploads?.ToList();
            Keys = keys;
            Portals = portals?.ToList();
        }

        public override string GetPath()
        {
            return "entities";
        }

        public override string GetLogContent()
        {
            return new { ModelId = ModelId, Data = Data, Portals = Portals, KeyId = KeyId, PacketId = PacketId }.Serialize();
        }
    }

    public class PostEntityResponse : ApiResponse
    {
        public Entity Entity { get; set; }

        public PostEntityResponse()  
        {
        }

        public PostEntityResponse(Entity entity)
        {
            Entity = entity;
        }
    }
}
