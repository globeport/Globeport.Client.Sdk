using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Globeport.Shared.Library.ClientModel;
using Globeport.Shared.Library.Extensions;

namespace Globeport.Shared.Library.ApiModel
{
    public class PutResource : ApiRequest
    {
        public string ResourceId { get; set; }
        public string Name { get; set; }
        public string Label { get; set; }
        public byte[] Data { get; set; }

        public PutResource()
        {
        }

        public PutResource(string resourceId, string name, string label, byte[] data)
        {
            ResourceId = resourceId;
            Name = name;
            Label = label;
            Data = data;
        }

        public override string GetPath()
        {
            return $"resources/{ResourceId}";
        }

        public override string GetLogContent()
        {
            return new { ResourceId = ResourceId, Name = Name, Label = Label }.Serialize();
        }
    }

    public class PutResourceResponse : ApiResponse
    {
        public Resource Resource { get; set; }
        public PutResourceResponse()
        {
        }

        public PutResourceResponse(Resource resource)
        {
            Resource = resource;
        }
    }
}
