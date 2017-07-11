using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using Globeport.Client.Net46.CLI.Attributes;

namespace Globeport.Client.Net46.CLI.Commands
{
    [Command("Updates an existing model dependency")]
    class UpdateModelDependency : Command
    {
        [Argument("The model Id")]
        public string ModelId { get; set; }

        [Argument("The dependency Id")]
        public string DependencyId { get; set; }

        protected async override Task<object> Execute()
        {
            return await Api.Client.PutModelDependency(ModelId, DependencyId);
        }
    }
}
