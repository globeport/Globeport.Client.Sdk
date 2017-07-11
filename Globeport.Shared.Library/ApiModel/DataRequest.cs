using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;

using Globeport.Shared.Library.Extensions;

namespace Globeport.Shared.Library.ApiModel
{
    public class DataRequest : ApiRequest
    {
        public DataCursor Cursor { get; set; }

        public DataRequest()
        {
        }

        public DataRequest(DataCursor cursor)
        {
            Cursor = cursor;
        }

        public override string GetQuery()
        {
            return Cursor == null ? string.Empty : $"position={Cursor.Position?.JoinCsv()}&direction={Cursor.Direction}&order={Cursor.Order}&pageSize={Cursor.PageSize}";
        }
    }
}
