using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Globeport.Client.Net46.CLI.Attributes;

namespace Globeport.Client.Net46.CLI.Commands
{
    [Command("Gets a key by its Id")]
    class GetKeyById : Command
    {
        [Argument("The key Id")]
        public string KeyId { get; set; }

        protected async override Task<object> Execute()
        {
            return await Api.Client.GetKey(KeyId);
        }
    }
}
