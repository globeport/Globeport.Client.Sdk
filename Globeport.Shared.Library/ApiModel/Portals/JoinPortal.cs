using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

using Globeport.Shared.Library.ClientModel;
using Globeport.Shared.Library.Extensions;

namespace Globeport.Shared.Library.ApiModel
{
    public class JoinPortal : ApiRequest
    {
        public string PortalId { get; set; }

        public JoinPortal()
        {
        }

        public JoinPortal(string portalId)
        {
            PortalId = portalId;
        }

        public override string GetPath()
        {
            return $"portals/{PortalId}/join";
        }

        public override string GetLogContent()
        {
            return new { PortalId = PortalId }.Serialize();
        }
    }

    public class JoinPortalResponse : ApiResponse
    {
        public Portal Portal { get; set; }

        public JoinPortalResponse() { }

        public JoinPortalResponse(Portal portal)
        {
            Portal = portal;
        }
    }
}
