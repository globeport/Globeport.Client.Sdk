using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Globeport.Client.Net46.CLI
{
    public class CommandArguments
    {
        public int CurrentIndex { get; private set; } = -1;
        string[] Args { get; }

        public CommandArguments(IEnumerable<string> args)
        {
            Args = args.ToArray();
        }

        public bool MoveNext()
        {
            if (CurrentIndex < Args.Length - 1) 
            {
                CurrentIndex++;
                return true;
            }
            return false;
        }

        public string Current
        {
            get
            {
                return Args[CurrentIndex];
            }
        }

        public int Count
        {
            get
            {
                return Args.Length;
            }
        }
    }
}
