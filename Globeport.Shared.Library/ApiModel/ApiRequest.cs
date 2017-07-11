using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Globeport.Shared.Library.ApiModel
{
    public class ApiRequest
    {
        public ApiRequest()
        {
        }

        public virtual string GetQuery()
        {
            return string.Empty;
        }

        public virtual string GetPath()
        {
            return string.Empty;
        }

        public virtual string GetLogContent()
        {
            return string.Empty;
        }
    }
}
