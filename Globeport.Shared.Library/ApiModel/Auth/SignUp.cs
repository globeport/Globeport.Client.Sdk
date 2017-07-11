using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Globeport.Shared.Library.Data;
using Globeport.Shared.Library.Extensions;

namespace Globeport.Shared.Library.ApiModel
{
    public class SignUp : ApiRequest
    {
        public string Version { get; set; }
        public string Username { get; set; }
        public string Color { get; set; }
        public byte[] Signature { get; set; }
        public byte[] Verifier { get; set; }
        public byte[] Salt { get; set; }
        public List<KeyUpload> Keys { get; set; }

        public SignUp()
        {
        }

        public SignUp(string version, string username, string color, byte[] signature, byte[] verifier, byte[] salt, IEnumerable<KeyUpload> keys)
        {
            Version = version;
            Username = username;
            Color = color;
            Signature = signature;
            Verifier = verifier;
            Salt = salt;
            Keys = keys.ToList();
        }

        public override string GetPath()
        {
            return "signup";
        }

        public override string GetLogContent()
        {
            return new { Username = Username, Color = Color, Version = Version }.Serialize();
        }
    }

    public class SignUpResponse : ApiResponse
    {
        public string SessionId { get; set; }
        public byte[] Credentials { get; set; }

        public SignUpResponse()
        {
        }

        public SignUpResponse(string sessionId, byte[] credentials)
        {
            SessionId = sessionId;
            Credentials = credentials;
        }
    }
}
