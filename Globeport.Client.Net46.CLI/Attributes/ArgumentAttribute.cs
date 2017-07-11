using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Globeport.Client.Net46.CLI.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class ArgumentAttribute : Attribute
    {
        public string Description { get; }
        public bool IsOptional { get; }
        public object DefaultValue { get; }

        public ArgumentAttribute(string description, bool isOptional = false, object defaultValue = null)
        {
            Description = description;
            IsOptional = isOptional;
            DefaultValue = defaultValue;
        }
    }
}
