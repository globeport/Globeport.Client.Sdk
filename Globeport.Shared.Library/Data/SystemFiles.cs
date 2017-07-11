using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Globeport.Shared.Library.Extensions;

namespace Globeport.Shared.Library.Data
{
    public class SystemFiles
    {
        public const string Moment = "3J2K2TFO0400";
        public const string Autolinker = "2HI24IN80400";

        public static Dictionary<string, string> All = typeof(SystemFiles).GetConstants();
    }
}
