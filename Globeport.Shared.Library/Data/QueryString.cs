using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Globeport.Shared.Library.Extensions;

namespace Globeport.Shared.Library.Data
{
    public class QueryString : Dictionary<string,string>
    {
        public QueryString() 
            : base(StringComparer.OrdinalIgnoreCase)
        {

        }
        public static QueryString Parse(string query)
        {
            var queryString = new QueryString();
            if (!query.IsNullOrEmpty())
            {
                var pairs = query.Split('&');
                foreach (var pair in pairs)
                {
                    var keyValue = pair.Split('=');
                    queryString.Add(keyValue[0], keyValue[1]);
                }
            }
            return queryString;
        }
    }
}
