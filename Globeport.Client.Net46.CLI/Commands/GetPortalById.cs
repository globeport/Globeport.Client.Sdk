﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Globeport.Client.Net46.CLI.Attributes;

namespace Globeport.Client.Net46.CLI.Commands
{
    [Command("Gets a portal by its Id")]
    class GetPortalById : Command
    {
        [Argument("The portal Id")]
        public string PortalId { get; set; }

        protected async override Task<object> Execute()
        {
            return await Api.Client.GetPortal(PortalId);
        }
    }
}
