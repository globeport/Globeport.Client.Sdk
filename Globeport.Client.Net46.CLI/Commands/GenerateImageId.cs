using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Globeport.Client.Net46.CLI.Attributes;

namespace Globeport.Client.Net46.CLI.Commands
{
    [Command("Generates a new image id")]
    class GenerateImageId : Command
    {
        protected override Task<object> Execute()
        {
            if (Api.Client.Session == null)
            {
                WriteError("You need to be logged in to generate an image Id");
                return Task.FromResult<object>(null);
            }
            return Task.FromResult((object)Api.Client.CryptoClient.GenerateId());
        }
    }
}
