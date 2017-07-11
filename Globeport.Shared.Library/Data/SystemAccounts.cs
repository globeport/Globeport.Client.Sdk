using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Globeport.Shared.Library.Extensions;

namespace Globeport.Shared.Library.Data
{
    public class SystemAccounts
    {
        public const string Globeport = "6HV273K0400";

        public static Dictionary<string, string> All = typeof(SystemAccounts).GetConstants().ToDictionary(i=>i.Key.ToUpperInvariant(), i=>i.Value);
    }
}
