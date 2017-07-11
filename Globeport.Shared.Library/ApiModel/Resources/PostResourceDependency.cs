using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Globeport.Shared.Library.ClientModel;
using Globeport.Shared.Library.Extensions;

namespace Globeport.Shared.Library.ApiModel
{
    public class PostResourceDependency : ApiRequest
    {
        public string ResourceId { get; set; }
        public string DependencyId { get; set; }

        public PostResourceDependency()
        {
        }

        public PostResourceDependency(string resourceId, string dependencyId)
        {
            ResourceId = resourceId;
            DependencyId = dependencyId;
        }

        public override string GetPath()
        {
            return $"resources/{ResourceId}/dependencies";
        }

        public override string GetLogContent()
        {
            return new { ResourceId = ResourceId, DependencyId = DependencyId }.Serialize();
        }
    }

    public class PostResourceDependencyResponse : ApiResponse
    {
        public Resource Resource { get; set; }

        public PostResourceDependencyResponse()
        {
        }

        public PostResourceDependencyResponse(Resource resource)
        {
            Resource = resource;
        }
    }
}
