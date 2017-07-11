using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using Globeport.Client.Net46.CLI.Attributes;

namespace Globeport.Client.Net46.CLI.Commands
{
    [Command("Closes the current session")]
    class LogOut : Command
    {
        [Argument("The session Id", true)]
        public string SessionId { get; set; }

        protected async override Task<object> Execute()
        {
            return await Api.Client.LogOut(SessionId);
        }
    }
}
