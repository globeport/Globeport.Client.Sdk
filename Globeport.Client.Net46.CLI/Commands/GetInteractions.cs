using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Globeport.Shared.Library.Data;
using Globeport.Shared.Library.ApiModel;
using Globeport.Client.Net46.CLI.Attributes;

namespace Globeport.Client.Net46.CLI.Commands
{
    [Command("Gets a list of interactions")]
    class GetInteractions : Command
    {
        [Argument("The entity Id")]
        public string EntityId { get; set; }

        [Argument("An account Id - used to filter interactions by account", true)]
        public string AccountId { get; set; }

        [Argument("The interaction type - used to filter interactions by type", true)]
        public string Type { get; set; }

        [Argument("A custom tag - used to filter interactions", true)]
        public string Tag { get; set; }

        [Argument("The cursor position (JSON array - [ClassName,EntityId])", true, new[] { "" })]
        public string[] Position { get; set; }

        [Argument("The cursor direction", true)]
        public CursorDirection Direction { get; set; }

        [Argument("The cursor order", true)]
        public CursorOrder Order { get; set; }

        [Argument("The cursor page size", true, 10)]
        public int PageSize { get; set; }

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

            var cursor = new DataCursor(Position, Direction, Order, PageSize);

            return await Api.Client.GetInteractions(entity, AccountId, Type, Tag, cursor, Dependencies);
        }
    }
}
