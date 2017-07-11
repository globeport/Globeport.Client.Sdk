using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Globeport.Shared.Library.Data;
using Globeport.Client.Net46.CLI.Attributes;

namespace Globeport.Client.Net46.CLI.Commands
{
    [Command("Clones an existing model")]
    class CloneModel : Command
    {
        [Argument("The model Id")]
        public string ModelId { get; set; }

        [Argument("The model name")]
        public string ModelName { get; set; }

        [Argument("The model label")]
        public string Label { get; set; }

        [Argument("The model color")]
        public string Color { get; set; }

        protected async override Task<object> Execute()
        {
            return await Api.Client.CloneModel(ModelId, ModelName, Label, Color, null);
        }
    }
}
