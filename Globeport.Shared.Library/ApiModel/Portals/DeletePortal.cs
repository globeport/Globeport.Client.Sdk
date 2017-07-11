using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Globeport.Shared.Library.Extensions;

namespace Globeport.Shared.Library.ApiModel
{
    public class DeletePortal : ApiRequest
    {
        public string Portal { get; set; }

        public DeletePortal()
        {
        }

        public DeletePortal(string portal)
        {
            Portal = portal;
        }

        public override string GetPath()
        {
            return $"portals/{Portal}";
        }

        public override string GetLogContent()
        {
            return new { Portal = Portal }.Serialize();
        }
    }

    public class DeletePortalResponse : ApiResponse
    {
        public DeletePortalResponse()  { }
    }
}
