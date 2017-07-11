using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Globeport.Client.Net46.CLI.Attributes;

namespace Globeport.Client.Net46.CLI.Commands
{
    [Command("Deletes an interaction")]
    class DeleteInteraction : Command
    {
        [Argument("The interaction Id")]
        public string InteractionId { get; set; }

        protected async override Task<object> Execute()
        {
            return await Api.Client.DeleteInteraction(InteractionId);
        }
    }
}
