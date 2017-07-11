using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using Globeport.Shared.Library.Data;
using Globeport.Client.Net46.CLI.Attributes;

namespace Globeport.Client.Net46.CLI.Commands
{
    [Command("Resets the notification counter")]
    class ResetCounter : Command
    {
        [Argument("The counter type")]
        public string Type { get; set; }

        protected async override Task<object> Execute()
        {
            return await Api.Client.ResetCounter(Type);
        }
    }
}
