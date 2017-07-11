using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Globeport.Shared.Library.ClientModel;
using Globeport.Shared.Library.Extensions;

namespace Globeport.Shared.Library.ApiModel
{
    public class DeleteResourceDependency : ApiRequest
    {
        public string ResourceId { get; set; }
        public string DependencyId { get; set; }

        public DeleteResourceDependency()
        {
        }

        public DeleteResourceDependency(string resourceId, string dependencyId)
        {
            ResourceId = resourceId;
            DependencyId = dependencyId;
        }

        public override string GetPath()
        {
            return $"resources/{ResourceId}/dependencies/{DependencyId}";
        }

        public override string GetLogContent()
        {
            return new { ResourceId = ResourceId, DependencyId = DependencyId }.Serialize();
        }
    }

    public class DeleteResourceDependencyResponse : ApiResponse
    {
        public Resource Resource { get; set; }

        public DeleteResourceDependencyResponse()
        {
        }

        public DeleteResourceDependencyResponse(Resource resource)
        {
            Resource = resource;
        }
    }
}
