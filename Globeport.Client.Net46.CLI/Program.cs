using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.IO;

using Newtonsoft.Json.Schema;

using Globeport.Client.Net46.CLI.Commands;
using Globeport.Shared.Library.Data;

namespace Globeport.Client.Net46.CLI
{
    class Program
    {
        public static Dictionary<string,Command> Commands { get; } = new Dictionary<string, Command>(StringComparer.OrdinalIgnoreCase);

        static void Main(string[] args)
        {
            License.RegisterLicense(Globals.JsonSchemaLicence);
            LoadCommands();
            Console.SetIn(new StreamReader(Console.OpenStandardInput(8192)));
            Console.WriteLine($"Globeport Api Console v{Assembly.GetExecutingAssembly().GetName().Version}");
            Console.WriteLine();
            Console.WriteLine($"Environment: {Api.AppSettings.Environment}");
            Console.WriteLine();
            Console.WriteLine("Enter 'help all' to list all available commands");
            Console.WriteLine();
            Console.Write("> ");
            while (true)
            {
                var input = Console.ReadLine();
                if (input != null)
                {
                    input = input.Trim();
                    if (input.ToUpperInvariant() == "EXIT") break;
                    if (input.Length > 0)
                    {
                        var commandArgs = input.Split(' ');
                        var commandName = commandArgs.First();
                        if (Commands.ContainsKey(commandName))
                        {
                            Commands[commandName].Execute(new CommandArguments(commandArgs.Skip(1)));
                        }
                    }
                        Console.Write("> ");
                }
            }
        }

        static void LoadCommands()
        {
            foreach (var type in typeof(Program).Assembly.GetTypes().Where(i => i.IsSubclassOf(typeof(Command))).OrderBy(i => i.Name))
            {
                var command = (Command)Activator.CreateInstance(type);
                Commands.Add(command.GetName().ToLower(), command);
            }
        }
    }
}
