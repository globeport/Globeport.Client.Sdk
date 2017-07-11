using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Globeport.Client.Net46.CLI.Attributes;

namespace Globeport.Client.Net46.CLI.Commands
{
    [Command("Gets a packet by its Id")]
    class GetPacketById : Command
    {
        [Argument("The packet Id")]
        public string PacketId { get; set; }

        protected async override Task<object> Execute()
        {
            return await Api.Client.GetPacket(PacketId);
        }
    }
}
