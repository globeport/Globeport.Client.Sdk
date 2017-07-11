using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Globeport.Shared.Library.Data
{
    public static class ValueSeparators
    {
        public static char[] Comma = new char[] { ',' };
        public static char[] Tab = new char[] { '\t' };
        public static char[] Pipe = new char[] { '|' };
        public static char[] SemiColon = new char[] { ';' };
        public static char[] At = new char[] { '@' };
        public static char[] NewLine = System.Environment.NewLine.ToCharArray();
        public static char[] DoubleHyphen = "--".ToCharArray();
        public static char[] Colon = new char[] { ':' };
    }
}
