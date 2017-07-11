using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;

using Globeport.Client.Net46.CLI.Attributes;
using System.Threading.Tasks;

namespace Globeport.Client.Net46.CLI.Commands
{
    [Command("Provides help for commands")]
    class Help : Command
    {
        [Argument("A command name ('all' lists all available commands)")]
        public string Command { get; set; }

        protected async override Task<object> Execute()
        {
            Console.WriteLine();

            if (Command == "all")
            {
                var length = Program.Commands.Values.Max(i => i.GetName().Length) + 10;
                foreach (var command in Program.Commands.Values)
                {
                    Console.WriteLine($"{command.GetName().PadRight(length)}{command.GetDescription()}");
                }
            }
            else if (Program.Commands.ContainsKey(Command))
            {
                var command = Program.Commands[Command];
                Console.WriteLine($"\nUsage:\n\t{command.GetUsage()}\n\t{string.Join("\n\t", command.GetParameters())}\n");
            }
            else
            {
                WriteError("Command not found");
            }

            Console.WriteLine();

            return null;
        }
    }
}