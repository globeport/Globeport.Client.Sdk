﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Globeport.Client.Net46.CLI.Attributes;

namespace Globeport.Client.Net46.CLI.Commands
{
    [Command("Gets a list of avatars")]
    class GetAvatarsById : Command
    {
        [Argument("A list of avatar Ids (JSON array)")]
        public string[] Avatars { get; set; }

        protected async override Task<object> Execute()
        {
            return await Api.Client.GetAvatars(Avatars);
        }
    }
}
