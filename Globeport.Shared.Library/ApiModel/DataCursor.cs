using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

using Globeport.Shared.Library.Data;

namespace Globeport.Shared.Library.ApiModel
{
    public class DataCursor
    {
        public string[] Position { get; set; }

        public CursorDirection Direction { get; set; }

        public CursorOrder Order { get; set; }

        public int PageSize { get; set; }

        public DataCursor()
        {
        }

        public DataCursor(string[] position, CursorDirection direction, CursorOrder order, int pageSize)
        {
            Position = position;
            Direction = direction;
            Order = order;
            PageSize = pageSize == 0 ? Globals.DefaultPageSize : pageSize;
        }
    }
}
