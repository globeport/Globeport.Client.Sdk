using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

using Globeport.Shared.Library.ClientModel;
using Globeport.Shared.Library.Extensions;

namespace Globeport.Shared.Library.ApiModel
{
    public class ChangePassword : ApiRequest
    {
        public string Version { get; set; }
        public byte[] Credentials { get; set; }
        public byte[] Evidence { get; set; }
        public byte[] Verifier { get; set; }
        public byte[] Salt { get; set; }
        public byte[] MasterKey { get; set; }

        public ChangePassword()
        {
        }

        public ChangePassword(string version, byte[] credentials, byte[] evidence, byte[] verifier, byte[] salt, byte[] masterKey)
        {
            Version = version;
            Credentials = credentials;
            Evidence = evidence;
            Verifier = verifier;
            Salt = salt;
            MasterKey = masterKey;
        }

        public override string GetPath()
        {
            return "password";
        }

        public override string GetLogContent()
        {
            return new { Version = Version }.Serialize();
        }
    }

    public class ChangePasswordResponse : ApiResponse
    {
        public byte[] Evidence { get; set; }
        public Account Account { get; set; }

        public ChangePasswordResponse()
        {
        }

        public ChangePasswordResponse(byte[] evidence, Account account)
        {
            Evidence = evidence;
            Account = account;
        }
    }
}
