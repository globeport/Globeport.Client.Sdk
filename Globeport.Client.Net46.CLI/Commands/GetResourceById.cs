using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Globeport.Client.Net46.CLI.Attributes;

namespace Globeport.Client.Net46.CLI.Commands
{
    [Command("Gets a resource by its Id")]
    class GetResourceById : Command
    {
        [Argument("The resource Id")]
        public string ResourceId { get; set; }

        [Argument("Return dependencies", true)]
        public bool Dependencies { get; set; }

        protected async override Task<object> Execute()
        {
            return await Api.Client.GetResource(ResourceId, Dependencies);
        }
    }
}
