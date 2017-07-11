using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Globeport.Shared.Library.Extensions;

namespace Globeport.Shared.Library.ApiModel
{
    public class LogIn : ApiRequest
    {
        public string Version { get; set; }
        public string Username { get; set; }

        public LogIn()
        {
        }
        public LogIn(string version, string username)
        {
            Version = version;
            Username = username;
        }

        public override string GetPath()
        {
            return "login";
        }

        public override string GetLogContent()
        {
            return new { Username = Username, Version = Version }.Serialize();
        }
    }

    public class LogInResponse : ApiResponse
    {
        public string SessionId { get; set; }
        public byte[] Salt { get; set; }
        public byte[] Credentials { get; set; }
        public string Version { get; set; }

        public LogInResponse()
        {
        }

        public LogInResponse(string sessionId, byte[] salt, byte[] credentials, string version)
        {
            SessionId = sessionId;
            Salt = salt;
            Credentials = credentials;
            Version = version;
        }
    }
}
