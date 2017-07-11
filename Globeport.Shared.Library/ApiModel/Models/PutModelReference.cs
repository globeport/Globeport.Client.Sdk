using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Globeport.Shared.Library.ClientModel;
using Globeport.Shared.Library.Extensions;

namespace Globeport.Shared.Library.ApiModel
{
    public class PutModelDependency : ApiRequest
    {
        public string ModelId { get; set; }
        public string DependencyId { get; set; }

        public PutModelDependency()
        {
        }

        public PutModelDependency(string modelId, string dependencyId)
        {
            ModelId = modelId;
            DependencyId = dependencyId;
        }

        public override string GetPath()
        {
            return $"models/{ModelId}/dependencies/{DependencyId}";
        }

        public override string GetLogContent()
        {
            return new { ModelId = ModelId, DependencyId = DependencyId }.Serialize();
        }
    }

    public class PutModelDependencyResponse : ApiResponse
    {
        public Model Model { get; set; }

        public PutModelDependencyResponse()
        {
        }

        public PutModelDependencyResponse(Model model)
        {
            Model = model;
        }
    }
}
