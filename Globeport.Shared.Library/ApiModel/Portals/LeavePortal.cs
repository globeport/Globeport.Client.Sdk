using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

using Globeport.Shared.Library.ClientModel;
using Globeport.Shared.Library.Extensions;

namespace Globeport.Shared.Library.ApiModel
{
    public class LeavePortal : ApiRequest
    {
        public string PortalId { get; set; }

        public LeavePortal()
        {
        }

        public LeavePortal(string portalId)
        {
            PortalId = portalId;
        }

        public override string GetPath()
        {
            return $"portals/{PortalId}/leave";
        }

        public override string GetLogContent()
        {
            return new { PortalId = PortalId }.Serialize();
        }
    }

    public class LeavePortalResponse : ApiResponse
    {
        public Portal Portal { get; set; }

        public LeavePortalResponse() { }

        public LeavePortalResponse(Portal portal)
        {
            Portal = portal;
        }
    }
}
