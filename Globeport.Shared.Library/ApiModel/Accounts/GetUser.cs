using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Globeport.Shared.Library.Extensions;

namespace Globeport.Shared.Library.ApiModel
{
    public class GetUser : ApiRequest
    {
        public string Username { get; set; }

        public GetUser()
        {
        }

        public GetUser(string username)
        {
            Username = username;
        }

        public override string GetPath()
        {
            return $"users/{Username}";
        }

        public override string GetLogContent()
        {
            return new { Username = Username }.Serialize();
        }
    }

    public class GetUserResponse : ApiResponse
    {
        public string AccountId { get; set; }

        public GetUserResponse()
        {
        }

        public GetUserResponse(string accountId)
        {
            AccountId = accountId;
        }
    }
}
