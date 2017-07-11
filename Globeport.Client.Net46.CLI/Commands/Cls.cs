using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Globeport.Client.Net46.CLI.Attributes;

namespace Globeport.Client.Net46.CLI.Commands
{
    [Command("Clears the console window")]
    class Cls : Command
    {
        protected async override Task<object> Execute()
        {
            Console.Clear();

            return null;
        }
    }
}
