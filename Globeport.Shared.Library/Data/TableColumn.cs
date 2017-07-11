using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Globeport.Shared.Library.Data
{ 
    public class TableColumn
    {
        public string Name { get; set; }
        public double Width { get; set; }

        public TableColumn()
        {
        }

        public TableColumn(string name)
            : this()
        {
            Name = name;
            Width = 200;
        }
    }
}
