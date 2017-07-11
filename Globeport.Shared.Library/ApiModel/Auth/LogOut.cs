using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Globeport.Shared.Library.Extensions;

namespace Globeport.Shared.Library.ApiModel
{
    public class LogOut : ApiRequest
    {
        public string SessionId { get; set; }

        public LogOut()
        {
        }

        public LogOut(string sessionId)
        {
            SessionId = sessionId;
        }

        public override string GetPath()
        {
            return "logout";
        }

        public override string GetLogContent()
        {
            return new { SessionId = SessionId }.Serialize();
        }
    }

    public class LogOutResponse : ApiResponse
    {
        public LogOutResponse()
        {
        }        
    }
}
