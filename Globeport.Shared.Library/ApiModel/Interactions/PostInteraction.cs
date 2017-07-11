using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Globeport.Shared.Library.ClientModel;
using Globeport.Shared.Library.Extensions;
using Globeport.Shared.Library.Data;

namespace Globeport.Shared.Library.ApiModel
{
    public class PostInteraction : ApiRequest
    {
        public string EntityId { get; set; }
        public string Type { get; set; }
        public string Tag { get; set; }
        public byte[] Data { get; set; }
        public byte[] Signature { get; set; }
        public bool IsPublic { get; set; }

        public PostInteraction()
        {
        }

        public PostInteraction(string entityId, string type, string tag, byte[] data, byte[] signature, bool isPublic)
        { 
            EntityId = entityId;
            Type = type;
            Tag = tag;
            Data = data;
            Signature = signature;
            IsPublic = isPublic;
        }

        public override string GetPath()
        {
            return $"entities/{EntityId}/interactions";
        }

        public override string GetLogContent()
        {
            return new { EntityId = EntityId, Type = Type, Data = Data, Signature = Signature, IsPublic = IsPublic }.Serialize();
        }
    }

    public class PostInteractionResponse : ApiResponse
    {
        public Interaction Interaction { get; set; }
        public Metadata Metadata { get; set; }

        public PostInteractionResponse()  
        {
        }

        public PostInteractionResponse(Interaction interaction, Metadata metadata)
        {
            Interaction = interaction;
            Metadata = metadata;
        }
    }
}
