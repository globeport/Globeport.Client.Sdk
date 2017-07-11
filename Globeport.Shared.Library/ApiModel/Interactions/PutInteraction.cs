using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Globeport.Shared.Library.ClientModel;
using Globeport.Shared.Library.Extensions;
using Globeport.Shared.Library.Data;

namespace Globeport.Shared.Library.ApiModel
{
    public class PutInteraction : ApiRequest
    {
        public string InteractionId { get; set; }
        public string Tag { get; set; }
        public byte[] Data { get; set; }
        public byte[] Signature { get; set; }
        public bool IsPublic { get; set; }

        public PutInteraction()
        {
        }

        public PutInteraction(string interactiveId, string tag, byte[] data, byte[] signature, bool isPublic)
        {
            InteractionId = interactiveId;
            Tag = tag;
            Data = data;
            Signature = signature;
            IsPublic = isPublic;
        }

        public override string GetPath()
        {
            return $"interactions/{InteractionId}";
        }

        public override string GetLogContent()
        {
            return new { InteractionId = InteractionId, Tag = Tag, Data = Data, Signature = Signature, IsPublic = IsPublic }.Serialize();
        }
    }

    public class PutInteractionResponse : ApiResponse
    {
        public Interaction Interaction { get; set; }
        public Metadata Metadata { get; set; }

        public PutInteractionResponse()  { }

        public PutInteractionResponse(Interaction interaction, Metadata metadata)
        {
            Interaction = interaction;
            Metadata = metadata;
        }
    }
}
