using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Globeport.Shared.Library.ClientModel;
using Globeport.Shared.Library.Extensions;

namespace Globeport.Shared.Library.ApiModel
{
    public class PostResource : ApiRequest
    {
        public string Name { get; set; }
        public string Label { get; set; }
        public string Type { get; set; }

        public PostResource()
        {
        }

        public PostResource(string name, string label, string type)
        {
            Name = name;
            Label = label;
            Type = type;
        }

        public override string GetPath()
        {
            return "resources";
        }

        public override string GetLogContent()
        {
            return new { Name = Name, Label = Label, Type = Type }.Serialize();
        }
    }

    public class PostResourceResponse : ApiResponse
    {
        public Resource Resource { get; set; }

        public PostResourceResponse()
        {
        }

        public PostResourceResponse(Resource resource)
        {
            Resource = resource;
        }
    }
}
