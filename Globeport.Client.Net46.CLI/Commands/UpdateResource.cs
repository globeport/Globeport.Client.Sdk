using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using Globeport.Shared.Library.Data;
using Globeport.Client.Net46.CLI.Attributes;

namespace Globeport.Client.Net46.CLI.Commands
{
    [Command("Updates an existing resource")]
    class UpdateResource : Command
    {
        [Argument("The resource Id")]
        public string ResourceId { get; set; }

        [Argument("The resource name")]
        public string ResourceName { get; set; }

        [Argument("The resource label")]
        public string Label { get; set; }

        [Argument("The resource data")]
        public string Data { get; set; }

        protected async override Task<object> Execute()
        {
            return await Api.Client.PutResource(ResourceId, ResourceName, Label, Data);
        }
    }
}
