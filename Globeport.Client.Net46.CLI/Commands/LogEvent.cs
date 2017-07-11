using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using Globeport.Shared.Library.Data;
using Globeport.Client.Net46.CLI.Attributes;

namespace Globeport.Client.Net46.CLI.Commands
{
    [Command("Logs a new event")]
    class LogEvent : Command
    {
        [Argument("The application name")]
        public string AppName { get; set; }

        [Argument("The event type")]
        public string Type { get; set; }

        [Argument("The event messsage")]
        public string Message { get; set; }

        [Argument("The event data", true)]
        public string Data { get; set; }

        protected async override Task<object> Execute()
        {
            return await Api.Client.LogEvent(AppName, Type, Message, Data);
        }
    }
}
