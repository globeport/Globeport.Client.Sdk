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
    [Command("Gets a list of models")]
    class GetModels : Command
    {
        [Argument("A class Id - filter models by class", true)]
        public string ClassId { get; set; }

        [Argument("The cursor position (JSON array - [Name,ModelId])", true, new[] { "" })]
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

            return await Api.Client.GetModels(cursor, ClassId);
        }
    }
}
