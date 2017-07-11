using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Globeport.Shared.Library.Extensions;

namespace Globeport.Shared.Library.ApiModel
{
    public class PutPushUri : ApiRequest
    {
        public string PushUri { get; set; }

        public PutPushUri()
        {
        }

        public PutPushUri(string pushUri)
        {
            PushUri = pushUri;
        }

        public override string GetPath()
        {
            return "pushuri";
        }

        public override string GetLogContent()
        {
            return new { PushUri = PushUri }.Serialize();
        }
    }

    public class PutPushUriResponse : ApiResponse
    {
        public PutPushUriResponse()  { }
    }
}
