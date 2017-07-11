using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using Globeport.Client.Net46.CLI.Attributes;

namespace Globeport.Client.Net46.CLI.Commands
{
    [Command("Leaves a portal")]
    class LeavePortal : Command
    {
        [Argument("The portal Id")]
        public string PortalId { get; set; }

        protected async override Task<object> Execute()
        {
            return await Api.Client.LeavePortal(PortalId);
        }
    }
}
