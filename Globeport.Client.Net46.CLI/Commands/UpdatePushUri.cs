﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using Globeport.Client.Net46.CLI.Attributes;

namespace Globeport.Client.Net46.CLI.Commands
{
    [Command("Updates the push uri")]
    class UpdatePushUri : Command
    {
        [Argument("The new push uri")]
        public string PushUri { get; set; }

        protected async override Task<object> Execute()
        {
            return await Api.Client.PutPushUri(PushUri);
        }
    }
}
