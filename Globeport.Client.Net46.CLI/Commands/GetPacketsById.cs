using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Globeport.Client.Net46.CLI.Attributes;

namespace Globeport.Client.Net46.CLI.Commands
{
    [Command("Gets a list of avatars")]
    class GetPacketsById : Command
    {
        [Argument("A list of packet Ids (JSON array)")]
        public string[] Packets { get; set; }

        protected async override Task<object> Execute()
        {
            return await Api.Client.GetPackets(Packets);
        }
    }
}
