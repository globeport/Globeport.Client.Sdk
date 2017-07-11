using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Globeport.Shared.Library.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class CompositePrimaryKeyAttribute : Attribute
    {
        public int Order { get; private set; }

        public CompositePrimaryKeyAttribute(int order)
        {
            this.Order = order;
        }
    }
}
