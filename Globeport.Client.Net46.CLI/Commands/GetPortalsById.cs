using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Globeport.Client.Net46.CLI.Attributes;

namespace Globeport.Client.Net46.CLI.Commands
{
    [Command("Gets a list of portals")]
    class GetPortalsById : Command
    {
        [Argument("A list of portal Ids (JSON array)")]
        public string[] Portals { get; set; }

        protected async override Task<object> Execute()
        {
            return await Api.Client.GetPortals(Portals);
        }
    }
}
