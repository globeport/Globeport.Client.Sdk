using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Globeport.Client.Net46.CLI.Attributes;

namespace Globeport.Client.Net46.CLI.Commands
{
    [Command("Gets a class by its Id")]
    class GetClassById : Command
    {
        [Argument("The class Id")]
        public string ClassId { get; set; }

        [Argument("Return dependencies", true)]
        public bool Dependencies { get; set; }

        protected async override Task<object> Execute()
        {
            return await Api.Client.GetClass(ClassId, Dependencies);
        }
    }
}
