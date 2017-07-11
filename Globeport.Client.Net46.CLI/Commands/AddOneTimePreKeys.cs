using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Globeport.Client.Net46.CLI.Attributes;

namespace Globeport.Client.Net46.CLI.Commands
{
    [Command("Adds a batch of one time pre keys")]
    class AddOneTimePreKeys : Command
    {
        protected async override Task<object> Execute()
        {
            var keys = Api.Client.GenerateOneTimePreKeys();

            return await Api.Client.PostKeys(keys);
        }
    }
}
