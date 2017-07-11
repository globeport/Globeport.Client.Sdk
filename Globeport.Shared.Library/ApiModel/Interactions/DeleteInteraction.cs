using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Globeport.Shared.Library.Extensions;
using Globeport.Shared.Library.Data;

namespace Globeport.Shared.Library.ApiModel
{
    public class DeleteInteraction : ApiRequest
    {
        public string InteractionId { get; set; }

        public DeleteInteraction()
        {
        }

        public DeleteInteraction(string interactionId)
        {
            InteractionId = interactionId;
        }

        public override string GetPath()
        {
            return $"interactions/{InteractionId}";
        }

        public override string GetLogContent()
        {
            return new { InteractionId = InteractionId }.Serialize();
        }
    }

    public class DeleteInteractionResponse : ApiResponse
    {
        public Metadata Metadata { get; set; }

        public DeleteInteractionResponse()
        {
        }

        public DeleteInteractionResponse(Metadata metadata)  
        {
            Metadata = metadata;
        }
    }
}
