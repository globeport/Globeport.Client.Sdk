using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Globeport.Shared.Library.ClientModel;
using Globeport.Shared.Library.Extensions;

namespace Globeport.Shared.Library.ApiModel
{
    public class DeleteModelDependency : ApiRequest
    {
        public string ModelId { get; set; }
        public string DependencyId { get; set; }

        public DeleteModelDependency()
        {
        }

        public DeleteModelDependency(string modelId, string dependencyId)
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

    public class DeleteModelDependencyResponse : ApiResponse
    {
        public Model Model { get; set; }

        public DeleteModelDependencyResponse()
        {
        }

        public DeleteModelDependencyResponse(Model model)
        {
            Model = model;
        }
    }
}
