using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization;

using Globeport.Shared.Library.Extensions;

namespace Globeport.Shared.Library.ApiModel
{
    public class AckMessages : ApiRequest
    {
        public string LastMessageId { get; set; }

        public AckMessages()
        {
        }

        public AckMessages(string lastMessageId)
        {
            LastMessageId = lastMessageId;
        }

        public override string GetPath()
        {
            return "messages";
        }

        public override string GetLogContent()
        {
            return new { LastMessageId = LastMessageId }.Serialize();
        }
    }

    public class AckMessagesResponse : ApiResponse
    {
        public AckMessagesResponse()
        {
        }
    }
}
