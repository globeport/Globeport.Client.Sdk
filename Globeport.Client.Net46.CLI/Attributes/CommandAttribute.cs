using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Globeport.Client.Net46.CLI.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class CommandAttribute : Attribute
    {
        public string Description { get; }

        public CommandAttribute(string description)
        {
            Description = description;
        }
    }
}
