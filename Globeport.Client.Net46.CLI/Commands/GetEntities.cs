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
    [Command("Gets a list of entities")]
    class GetEntities : Command
    {
        [Argument("The portal Id")]
        public string PortalId { get; set; }

        [Argument("A model Id - used to filter entities by model", true)]
        public string ModelId { get; set; }

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
            var cursor = new DataCursor(Position, Direction, Order, PageSize);

            var response = await Api.Client.GetEntities(cursor, PortalId, ModelId, Dependencies);

            var tasks = response.Entities.Select(i => GetEntityData(i));

            Task.WhenAll(tasks).Wait();

            return response;
        }
    }
}
