using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization;

namespace Globeport.Shared.Library.ApiModel
{
    public class Ping : ApiRequest
    {
        public Ping()
        {
        }

        public override string GetPath()
        {
            return "admin/ping";
        }
    }

    public class PingResponse : ApiResponse
    {
        public DateTimeOffset Timestamp { get; set; }

        public PingResponse()
        {
        }

        public PingResponse(DateTimeOffset timestamp)
        {
            Timestamp = timestamp;
        }
    }
}
