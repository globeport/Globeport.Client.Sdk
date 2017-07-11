using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization;

using Globeport.Shared.Library.Data;
using Globeport.Shared.Library.Extensions;

namespace Globeport.Shared.Library.ApiModel
{
    public class GetKeyBundle : ApiRequest
    {
        public string AccountId { get; set; }

        public GetKeyBundle()
        {
        }

        public GetKeyBundle(string accountId)
        {
            AccountId = accountId;
        }

        public override string GetPath()
        {
            return $"keys/{AccountId}/bundle";
        }

        public override string GetLogContent()
        {
            return new { AccountId = AccountId }.Serialize();
        }
    }

    public class GetKeyBundleResponse : ApiResponse
    {
        public KeyBundle KeyBundle { get; set; }

        public GetKeyBundleResponse()
        {
        }

        public GetKeyBundleResponse(KeyBundle keyBundle)
        {
            KeyBundle = keyBundle;
        }
    }
}
