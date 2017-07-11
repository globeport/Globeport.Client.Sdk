using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Globeport.Shared.Library.ClientModel;
using Globeport.Shared.Library.Extensions;

namespace Globeport.Shared.Library.ApiModel
{
    public class DeleteModel : ApiRequest
    {
        public string ModelId { get; set; }

        public DeleteModel()
        {
        }

        public DeleteModel(string modelId)
        {
            ModelId = modelId;
        }

        public override string GetPath()
        {
            return $"models/{ModelId}";
        }

        public override string GetLogContent()
        {
            return new { ModelId = ModelId }.Serialize();
        }
    }

    public class DeleteModelResponse : ApiResponse
    { 
        public Class Class { get; set; }

        public DeleteModelResponse()
        {
        }

        public DeleteModelResponse(Class @class)
        {
            Class = @class;
        }
    }
}
