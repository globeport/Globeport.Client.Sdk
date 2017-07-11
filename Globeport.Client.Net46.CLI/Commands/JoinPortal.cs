using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using Globeport.Client.Net46.CLI.Attributes;

namespace Globeport.Client.Net46.CLI.Commands
{
    [Command("Joins a portal")]
    class JoinPortal : Command
    {
        [Argument("The portal Id")]
        public string PortalId { get; set; }

        protected async override Task<object> Execute()
        {
            return await Api.Client.JoinPortal(PortalId);
        }
    }
}
