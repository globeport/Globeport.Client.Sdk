using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Globeport.Client.Net46.CLI.Attributes;

namespace Globeport.Client.Net46.CLI.Commands
{
    [Command("Gets a list of interactions")]
    class GetInteractionsById : Command
    {
        [Argument("The entity Id")]
        public string EntityId { get; set; }

        [Argument("A list of interaction Ids (JSON array)")]
        public string[] Interactions { get; set; }

        [Argument("Return dependencies", true)]
        public bool Dependencies { get; set; }

        protected async override Task<object> Execute()
        {
            var response = await Api.Client.GetEntity(EntityId, false);

            var entity = response.Entities.FirstOrDefault();

            if (entity == null)
            {
                WriteError("The entity doesn't exist");
                return null;
            }

            return await Api.Client.GetInteractions(entity, Interactions, Dependencies);
        }
    }
}
