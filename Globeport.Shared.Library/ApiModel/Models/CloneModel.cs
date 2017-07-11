using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Globeport.Shared.Library.ClientModel;
using Globeport.Shared.Library.Data;
using Globeport.Shared.Library.Extensions;

namespace Globeport.Shared.Library.ApiModel
{
    public class CloneModel : ApiRequest
    {
        public string ModelId { get; set; }
        public string Name { get; set; }
        public string Label { get; set; }
        public string Color { get; set; }
        public MediaUpload ImageUpload { get; set; }

        public CloneModel()
        {
        }

        public CloneModel(string modelId, string name, string label, string color, MediaUpload imageUpload)
        {
            ModelId = modelId;
            Name = name;
            Label = label;
            Color = color;
            ImageUpload = imageUpload;
        }

        public override string GetPath()
        {
            return $"models/{ModelId}/clone";
        }

        public override string GetLogContent()
        {
            return new { ModelId = ModelId, Name = Name, Label = Label, Color = Color }.Serialize();
        }
    }

    public class CloneModelResponse : ApiResponse
    {
        public Model Model { get; set; }

        public CloneModelResponse()
        {
        }

        public CloneModelResponse(Model model)
        {
            Model = model;
        }
    }
}
