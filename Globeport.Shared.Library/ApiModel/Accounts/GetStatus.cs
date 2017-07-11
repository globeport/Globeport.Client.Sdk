using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization;


namespace Globeport.Shared.Library.ApiModel
{
    public class GetStatus : ApiRequest
    { 
        public GetStatus()
        {
        }

        public override string GetPath()
        {
            return "status";
        }
    }

    public class GetStatusResponse : ApiResponse
    {
        public bool IsMessageQueueEmpty { get; set; }
        public bool IsOneTimePreKeyCountLow { get; set; }

        public GetStatusResponse()
        {
        }

        public GetStatusResponse(bool isMessageQueueEmpty, bool isOneTimePreKeyCount)
        {
            IsMessageQueueEmpty = isMessageQueueEmpty;
            IsOneTimePreKeyCountLow = isOneTimePreKeyCount;
        }
    }
}
