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
    [Command("Gets a list of portals")]
    class GetPortals : Command
    {
        [Argument("A contact Id - filter or select portals by contact", true)]
        public string ContactId { get; set; }

        [Argument("A entity Id - filter or select portals by entity", true)]
        public string EntityId { get; set; }

        [Argument("A list of portal types (JSON array) - filter portals by type", true)]
        public string[] Types { get; set; }

        [Argument("A list of portal states (JSON array) - filter portals by state", true)]
        public string[] States { get; set; }

        [Argument("The result set mode", true)]
        public string Mode { get; set; }

        [Argument("The cursor position (JSON array - [Name,PortalId])", true, new[] { "" })]
        public string[] Position { get; set; }

        [Argument("The cursor direction", true)]
        public CursorDirection Direction { get; set; }

        [Argument("The cursor order", true)]
        public CursorOrder Order { get; set; }

        [Argument("The cursor page size", true, 10)]
        public int PageSize { get; set; }

        protected async override Task<object> Execute()
        {
            var cursor = new DataCursor(Position, Direction, Order, PageSize);

            return await Api.Client.GetPortals(cursor, ContactId, EntityId, Types, States, Mode);
        }
    }
}
