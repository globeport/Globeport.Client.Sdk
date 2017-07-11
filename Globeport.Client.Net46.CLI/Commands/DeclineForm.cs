using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using Globeport.Client.Net46.CLI.Attributes;

namespace Globeport.Client.Net46.CLI.Commands
{
    [Command("Declines an outstanding form")]
    class DeclineForm : Command
    {
        [Argument("The form Id")]
        public string FormId { get; set; }

        protected async override Task<object> Execute()
        {
            return await Api.Client.DeclineForm(FormId);
        }
    }
}
