using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Globeport.Shared.Library.Data
{
    public class Html
    {
        public string Url { get; set; }
        public string Document { get; set; }

        public Html(string url, string document)
        {
            Url = url;
            Document = document;
        }
    }
}
