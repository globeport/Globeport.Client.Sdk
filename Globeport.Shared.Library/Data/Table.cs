using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;


namespace Globeport.Shared.Library.Data
{
    public class Table
    {
        public TableColumn[] Columns { get; set; }
        public Dictionary<string,string>[] Rows { get; set; }

        public Table()
        {
        }

        public Table(IEnumerable<TableColumn> columns)
        {
            Columns = columns.ToArray();
            Rows = new Dictionary<string, string>[0];
        }

        public Table(IEnumerable<TableColumn> columns, IEnumerable<Dictionary<string, string>> rows)
        {
            Columns = columns.ToArray();
            Rows = rows.ToArray();
        }
    }
}
