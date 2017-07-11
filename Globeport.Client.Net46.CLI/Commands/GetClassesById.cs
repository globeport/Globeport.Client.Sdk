using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Globeport.Client.Net46.CLI.Attributes;

namespace Globeport.Client.Net46.CLI.Commands
{
    [Command("Gets a list of classes")]
    class GetClassesById : Command
    {
        [Argument("A list of class Ids (JSON array)")]
        public string[] Classes { get; set; }

        [Argument("Return class dependencies", true)]
        public bool Dependencies { get; set; }

        protected async override Task<object> Execute()
        {
            return await Api.Client.GetClasses(Classes, Dependencies);
        }
    }
}
