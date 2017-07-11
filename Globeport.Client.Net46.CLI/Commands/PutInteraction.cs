using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Globeport.Client.Net46.CLI.Attributes;
using Globeport.Shared.Library.Extensions;
using Globeport.Shared.Library.Components;

namespace Globeport.Client.Net46.CLI.Commands
{
    [Command("Updates an interaction")]
    class PutInteraction : Command
    {
        [Argument("The entity Id")]
        public string EntityId { get; set; }

        [Argument("The interaction Id")]
        public string InteractionId { get; set; }

        [Argument("A custom tag")]
        public string Tag { get; set; }

        [Argument("The interaction data")]
        public string Data { get; set; }

        [Argument("Set true to encrypt the interaction", true)]
        public bool Encrypt { get; set; }

        protected async override Task<object> Execute()
        {
            var entityResponse = await Api.Client.GetEntity(EntityId, false);

            var entity = entityResponse.Entities.FirstOrDefault();

            if (entity == null)
            {
                WriteError("The entity doesn't exist");
                return null;
            }

            return await Api.Client.PutInteraction(entity, InteractionId, Tag, Data.Deserialize<DataObject>(), Encrypt);
        }
    }
}
