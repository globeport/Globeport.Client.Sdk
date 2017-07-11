using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Globeport.Shared.Library.Data
{
    public class AutoLinkMatch
    {
        public string matchedText { get; set; }
        public int offset { get; set; }
        public string url { get; set; }
        public string email { get; set; }
        public string mention { get; set; }
        public string hashtag { get; set; }
        public string phone { get; set; }
    }
}
