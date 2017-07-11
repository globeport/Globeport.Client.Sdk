using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Globeport.Shared.Library.Data;
using Globeport.Shared.Library.Extensions;

namespace Globeport.Shared.Library.ApiModel
{
    public class LogEvent : ApiRequest
    {
        public string AppName { get; set; }
        public string EventType { get; set; }
        public string Message { get; set; }
        public string Data { get; set; }

        public LogEvent()
        {
        }

        public LogEvent(string appName, string type, string message, string data)
        {
            AppName = appName;
            EventType = type;
            Message = message;
            Data = data;
        }

        public override string GetPath()
        {
            return "analytics/events";
        }

        public override string GetLogContent()
        {
            return new { AppName = AppName, EventType = EventType, Message = Message, Data = Data }.Serialize();
        }
    }

    public class LogEventResponse : ApiResponse
    {
        public LogEventResponse() { }
    }
}
