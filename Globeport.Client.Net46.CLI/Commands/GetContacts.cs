using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Globeport.Shared.Library.ApiModel;
using Globeport.Shared.Library.Data;
using Globeport.Client.Net46.CLI.Attributes;

namespace Globeport.Client.Net46.CLI.Commands
{
    [Command("Gets a list of contacts")]
    class GetContacts : Command
    {
        [Argument("A portal Id - filter or select contacts by portal", true)]
        public string PortalId { get; set; }

        [Argument("The result set mode", true)]
        public string Mode { get; set; }

        [Argument("The cursor position (JSON array - [Name,ContactId])", true, new[] { "" })]
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

            return await Api.Client.GetContacts(cursor, PortalId, Mode);
        }
    }
}
