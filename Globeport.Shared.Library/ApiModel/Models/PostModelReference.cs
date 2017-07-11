using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Globeport.Shared.Library.ClientModel;
using Globeport.Shared.Library.Extensions;

namespace Globeport.Shared.Library.ApiModel
{
    public class PostModelDependency : ApiRequest
    {
        public string ModelId { get; set; }
        public string DependencyId { get; set; }

        public PostModelDependency()
        {
        }

        public PostModelDependency(string modelId, string dependencyId)
        {
            ModelId = modelId;
            DependencyId = dependencyId;
        }

        public override string GetPath()
        {
            return $"models/{ModelId}/dependencies";
        }

        public override string GetLogContent()
        {
            return new { ModelId = ModelId, DependencyId = DependencyId }.Serialize();
        }
    }

    public class PostModelDependencyResponse : ApiResponse
    {
        public Model Model { get; set; }

        public PostModelDependencyResponse()
        {
        }

        public PostModelDependencyResponse(Model model)
        {
            Model = model;
        }
    }
}
