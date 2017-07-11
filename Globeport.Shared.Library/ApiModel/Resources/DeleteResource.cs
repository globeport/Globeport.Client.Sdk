using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Globeport.Shared.Library.ClientModel;
using Globeport.Shared.Library.Extensions;

namespace Globeport.Shared.Library.ApiModel
{
    public class DeleteResource : ApiRequest
    {
        public string ResourceId { get; set; }

        public DeleteResource()
        {
        }

        public DeleteResource(string resourceId)
        {
            ResourceId = resourceId;
        }

        public override string GetPath()
        {
            return $"resources/{ResourceId}";
        }

        public override string GetLogContent()
        {
            return new { ResourceId = ResourceId }.Serialize();
        }
    }

    public class DeleteResourceResponse : ApiResponse
    {
        public DeleteResourceResponse()
        {
        }
    }
}
