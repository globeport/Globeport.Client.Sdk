using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Globeport.Shared.Library.Extensions;

namespace Globeport.Shared.Library.Data
{
    public class SystemTables
    {
        public const string Strings = nameof(Strings);

        public static Dictionary<string, string> All = typeof(SystemTables).GetConstants();
    }
}
