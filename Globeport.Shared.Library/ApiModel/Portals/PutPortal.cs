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
    public class PutPortal : ApiRequest
    {
        public string PortalId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Color { get; set; }
        public MediaUpload ImageUpload { get; set; }

        public PutPortal()
        {
        }

        public PutPortal(string portalId, string name, string description, string color, MediaUpload imageUpload)
        {
            PortalId = portalId;
            Name = name;
            Description = description;
            Color = color;
            ImageUpload = imageUpload;
        }

        public override string GetPath()
        {
            return $"portals/{PortalId}";
        }

        public override string GetLogContent()
        {
            return new { PortalId = PortalId, Name = Name, Description = Description, Color = Color }.Serialize();
        }
    }

    public class PutPortalResponse : ApiResponse
    {
        public Portal Portal { get; set; }

        public PutPortalResponse()
        {
        }

        public PutPortalResponse(Portal portal)
        {
            Portal = portal;
        }
    }
}
