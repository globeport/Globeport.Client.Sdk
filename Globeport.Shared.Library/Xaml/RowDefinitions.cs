using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Globeport.Shared.Library.Xaml
{
    public class RowDefinitions : List<RowDefinition>
    {
        public RowDefinitions()
        {
        }

        public RowDefinitions(IEnumerable<RowDefinition> rows): base(rows)
        {
        }
    }
}
