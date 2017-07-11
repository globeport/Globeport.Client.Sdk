using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Globeport.Client.Net46.CLI.Attributes;

namespace Globeport.Client.Net46.CLI.Commands
{
    [Command("Gets a list of entities")]
    class GetEntitiesById : Command
    {
        [Argument("A list of entity Ids (JSON array)")]
        public string[] Entities { get; set; }

        [Argument("Return dependencies", true)]
        public bool Dependencies { get; set; }

        protected async override Task<object> Execute()
        {
            var response = await Api.Client.GetEntities(Entities, Dependencies);

            var tasks = response.Entities.Select(i => GetEntityData(i));

            Task.WhenAll(tasks).Wait();

            return response;
        }
    }
}
