using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Globeport.Shared.Library.ClientModel;
using Globeport.Shared.Library.Extensions;

namespace Globeport.Shared.Library.ApiModel
{
    public class CloneResource : ApiRequest
    {
        public string ModelId { get; set; }
        public string ResourceId { get; set; }

        public CloneResource()
        {
        }

        public CloneResource(string modelId, string resourceId)
        {
            ModelId = modelId;
            ResourceId = resourceId;
        }

        public override string GetPath()
        {
            return $"models/{ModelId}/resources/{ResourceId}/clone";
        }

        public override string GetLogContent()
        {
            return new { ModelId = ModelId, ResourceId = ResourceId }.Serialize();
        }
    }

    public class CloneResourceResponse : ApiResponse
    {
        public Resource Resource { get; set; }
        public Model Model { get; set; }

        public CloneResourceResponse()
        {
        }

        public CloneResourceResponse(Model model, Resource resource)
        {
            Model = model;
            Resource = resource;
        }
    }
}
