﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Globeport.Client.Net46.CLI.Attributes;

namespace Globeport.Client.Net46.CLI.Commands
{
    [Command("Gets a model by its Id")]
    class GetModelById : Command
    {
        [Argument("The model Id")]
        public string ModelId { get; set; }

        protected async override Task<object> Execute()
        {
            return await Api.Client.GetModel(ModelId);
        }
    }
}
