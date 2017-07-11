﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Globeport.Client.Net46.CLI.Attributes;

namespace Globeport.Client.Net46.CLI.Commands
{
    [Command("Gets a contact by their Id")]
    class GetContactById : Command
    {
        [Argument("The contact Id")]
        public string ContactId { get; set; }

        protected async override Task<object> Execute()
        {
            return await Api.Client.GetContactById(ContactId);
        }
    }
}
