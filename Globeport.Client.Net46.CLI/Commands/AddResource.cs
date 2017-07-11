using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Globeport.Shared.Library.Data;
using Globeport.Client.Net46.CLI.Attributes;

namespace Globeport.Client.Net46.CLI.Commands
{
    [Command("Adds a new resource")]
    class AddResource : Command
    {
        [Argument("The resource type")]
        public string Type { get; set; }

        [Argument("The resource name")]
        public string ResourceName { get; set; }

        [Argument("The resource label")]
        public string Label { get; set; }

        protected async override Task<object> Execute()
        {
            return await Api.Client.PostResource(ResourceName, Label, Type);
        }
    }
}
