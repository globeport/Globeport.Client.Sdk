using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Globeport.Shared.Library.ApiModel
{
    public class Challenge : ApiRequest
    {
        public Challenge()
        {
        }

        public override string GetPath()
        {
            return "challenge";
        }
    }

    public class ChallengeResponse : ApiResponse
    {
        public byte[] Credentials { get; set; }

        public ChallengeResponse()  { }

        public ChallengeResponse(byte[] credentials)
        {
            Credentials = credentials;
        }
    }
}
