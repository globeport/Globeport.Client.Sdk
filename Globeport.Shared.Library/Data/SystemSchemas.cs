using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Globeport.Shared.Library.Extensions;

namespace Globeport.Shared.Library.Data
{
    public class SystemSchemas
    {
        public const string Model = nameof(Model);

        public static Dictionary<string, string> All = typeof(SystemSchemas).GetConstants();
    }
}
