using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using Globeport.Client.Net46.CLI.Attributes;

namespace Globeport.Client.Net46.CLI.Commands
{
    [Command("Deletes a list of keys")]
    class DeleteKeys : Command
    {
        [Argument("A list of key Ids (JSON array)")]
        public string[] Keys { get; set; }

        protected async override Task<object> Execute()
        {
            await Api.Client.DeleteKeys(Keys);

            return string.Empty;
        }
    }
}
