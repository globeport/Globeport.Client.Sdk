using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using Globeport.Client.Net46.CLI.Attributes;

namespace Globeport.Client.Net46.CLI.Commands
{
    [Command("Clones an existing resource")]
    class CloneResource : Command
    {
        [Argument("The resource Id")]
        public string ResourceId { get; set; }

        [Argument("The model Id")]
        public string ModelId { get; set; }

        protected async override Task<object> Execute()
        {
            return await Api.Client.CloneResource(ModelId, ResourceId);
        }
    }
}
