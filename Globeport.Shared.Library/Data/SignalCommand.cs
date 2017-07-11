using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Globeport.Shared.Library.Interop;

namespace Globeport.Shared.Library.Data
{
    public class SignalCommand
    {
        public string Command { get; set; }
        public string[] Parameters { get; set; }

        public SignalCommand()
        {
        }

        public SignalCommand(string command, params string[] parameters)
        {
            Command = command;
            Parameters = parameters;
        }
    }
}
