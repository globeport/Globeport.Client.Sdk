﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Globeport.Client.Net46.CLI.Attributes;

namespace Globeport.Client.Net46.CLI.Commands
{
    [Command("Gets the session status")]
    class GetStatus : Command
    {
        protected async override Task<object> Execute()
        {
            return await Api.Client.GetStatus();
        }
    }
}
