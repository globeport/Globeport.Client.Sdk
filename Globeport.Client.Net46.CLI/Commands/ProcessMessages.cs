using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using Globeport.Client.Net46.CLI.Attributes;

namespace Globeport.Client.Net46.CLI.Commands
{
    [Command("Processes the list incoming of messages")]
    class ProcessMessages : Command
    {
        protected async override Task<object> Execute()
        {
            await Api.Client.ProcessMessages();

            return string.Empty;
        }
    }
}
