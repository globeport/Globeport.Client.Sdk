using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Globeport.Client.Net46.CLI.Attributes;

namespace Globeport.Client.Net46.CLI.Commands
{
    [Command("Gets an entity by its Id")]
    class GetEntityById : Command
    {
        [Argument("The entity Id")]
        public string EntityId { get; set; }

        [Argument("Return dependencies", true)]
        public bool Dependencies { get; set; }

        protected async override Task<object> Execute()
        {
            var response = await Api.Client.GetEntity(EntityId, Dependencies);

            await GetEntityData(response.Entities[0]);

            return response;
        }
    }
}
