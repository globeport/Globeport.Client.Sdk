﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Globeport.Shared.Library.Data;
using Globeport.Shared.Library.ApiModel;
using Globeport.Client.Net46.CLI.Attributes;

namespace Globeport.Client.Net46.CLI.Commands
{
    [Command("Gets a list of avatars")]
    class GetAvatars : Command
    {
        [Argument("A search term - search by name and username", true)]
        public string Search { get; set; }

        [Argument("An accountId - filter avatars by account", true)]
        public string AccountId { get; set; }

        [Argument("A portal Id - add 'IsMember' field to resultset (set to true when user is a member of the portal)", true)]
        public string PortalId { get; set; }

        [Argument("The cursor position (JSON array - [Name,AvatarId])", true, new[] { "" })]
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

            return await Api.Client.GetAvatars(cursor, Search, AccountId, PortalId);
        }
    }
}
