using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Globeport.Shared.Library.ClientModel;

namespace Globeport.Shared.Library.ApiModel
{
    public class GetAccount : ApiRequest
    {
        public GetAccount()
        {
        }

        public override string GetPath()
        {
            return "account";
        }
    }

    public class GetAccountResponse : ApiResponse
    {
        public Account Account { get; set; }

        public GetAccountResponse()
        {
        }

        public GetAccountResponse(Account account)
        {
            Account = account;
        }
    }
}
