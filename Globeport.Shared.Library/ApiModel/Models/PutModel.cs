using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Globeport.Shared.Library.ClientModel;
using Globeport.Shared.Library.Data;
using Globeport.Shared.Library.Extensions;

namespace Globeport.Shared.Library.ApiModel
{
    public class PutModel : ApiRequest
    {
        public string ModelId { get; set; }
        public string Name { get; set; }
        public string Label { get; set; }
        public string Color { get; set; }
        public bool IsInteractive { get; set; }
        public MediaUpload ImageUpload { get; set; }

        public PutModel()
        {
        }

        public PutModel(string modelId, string name, string label, string color, bool isInteractive, MediaUpload imageUpload)
        {
            ModelId = modelId;
            Name = name;
            Label = label;
            Color = color;
            IsInteractive = isInteractive;
            ImageUpload = imageUpload;
        }

        public override string GetPath()
        {
            return $"models/{ModelId}";
        }

        public override string GetLogContent()
        {
            return new { ModelId = ModelId, Name = Name, Label = Label, Color = Color, IsInteractive = IsInteractive }.Serialize();
        }
    }

    public class PutModelResponse : ApiResponse
    {
        public Model Model { get; set; }

        public PutModelResponse()
        {
        }

        public PutModelResponse(Model model)
        {
            Model = model;
        }
    }
}
