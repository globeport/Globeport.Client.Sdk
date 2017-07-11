using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Globeport.Shared.Library.ClientModel;
using Globeport.Shared.Library.Data;
using Globeport.Shared.Library.Extensions;

namespace Globeport.Shared.Library.ApiModel
{
    public class PostModel : ApiRequest
    {
        public string Name { get; set; }
        public string Label { get; set; }
        public string Color { get; set; }
        public bool IsInteractive { get; set; }
        public MediaUpload ImageUpload { get; set; }

        public PostModel()
        {
        }

        public PostModel(string name, string label, string color, bool isInteractive, MediaUpload imageUpload)
        {
            Name = name;
            Label = label;
            Color = color;
            IsInteractive = isInteractive;
            ImageUpload = imageUpload;
        }

        public override string GetPath()
        {
            return "models";
        }

        public override string GetLogContent()
        {
            return new { Name = Name, Label = Label, Color = Color, IsInteractive = IsInteractive }.Serialize();
        }
    }

    public class PostModelResponse : ApiResponse
    {
        public Class Class { get; set; }
        public Model Model { get; set; }
        public List<Resource> Resources { get; set; }

        public PostModelResponse()
        {
        }

        public PostModelResponse(Class @class, Model model, List<Resource> resources)
        {
            Class = @class;
            Model = model;
            Resources = resources;
        }
    }
}
