using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Globeport.Shared.Library.Xaml
{
    public class ColumnDefinitions : List<ColumnDefinition>
    {
        public ColumnDefinitions()
        {
        }

        public ColumnDefinitions(IEnumerable<ColumnDefinition> columns)
            : base(columns)
        {
        }
    }
}
