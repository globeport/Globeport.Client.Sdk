using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization;

using Globeport.Shared.Library.ClientModel;

namespace Globeport.Shared.Library.ApiModel
{
    public class GetMessages : ApiRequest
    {
        public GetMessages()
        {
        }

        public override string GetPath()
        {
            return "messages";
        }
    }

    public class GetMessagesResponse : ApiResponse
    {
        public List<SignalMessage> Messages { get; set; }

        public bool IsQueueEmpty { get; set; }

        public GetMessagesResponse()
        {
        }

        public GetMessagesResponse(List<SignalMessage> messages, bool isQueueEmpty)
        {
            Messages = messages;
            IsQueueEmpty = isQueueEmpty;
        }
    }
}
