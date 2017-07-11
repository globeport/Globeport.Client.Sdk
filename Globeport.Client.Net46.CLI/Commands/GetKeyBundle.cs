using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Globeport.Client.Net46.CLI.Attributes;

namespace Globeport.Client.Net46.CLI.Commands
{
    [Command("Gets a key bundle")]
    class GetKeyBundle : Command
    {
        [Argument("An account Id")]
        public string AccountId { get; set; }

        protected async override Task<object> Execute()
        {
            return await Api.Client.GetKeyBundle(AccountId);
        }
    }
}
