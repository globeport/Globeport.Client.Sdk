using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

using Globeport.Shared.Library.ClientModel;
using Globeport.Shared.Library.Data;
using Globeport.Shared.Library.Extensions;

namespace Globeport.Shared.Library.ApiModel
{
    public class PostPortal : ApiRequest
    {
        public string Type { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Color { get; set; }
        public MediaUpload ImageUpload { get; set; }

        public PostPortal()
        {
        }

        public PostPortal(string type, string name, string description, string color, MediaUpload imageUpload)
        {
            Type = type;
            Name = name;
            Description = description;
            Color = color;
            ImageUpload = imageUpload;
        }

        public override string GetPath()
        {
            return "portals";
        }

        public override string GetLogContent()
        {
            return new { Type = Type, Name = Name, Description = Description, Color = Color }.Serialize();
        }
    }

    public class PostPortalResponse : ApiResponse
    {
        public Portal Portal { get; set; }

        public PostPortalResponse()
        {
        }

        public PostPortalResponse(Portal portal)
        {
            Portal = portal;
        }
    }
}
