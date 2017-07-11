using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Globeport.Shared.Library.Xaml
{
    public class DependencyObjects : Dictionary<string, DependencyObject>
    {
        public DependencyObjects()
        {
        }

        public DependencyObjects(IDictionary<string, DependencyObject> elements) : base(elements)
        {
        }
    }
}
