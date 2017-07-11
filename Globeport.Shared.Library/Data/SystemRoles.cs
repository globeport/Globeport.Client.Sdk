using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Globeport.Shared.Library.Extensions;

namespace Globeport.Shared.Library.Data
{
    public class SystemRoles
    {
        public const string User = "User";
        public const string Administrator = "Administrator";

        public static Dictionary<string, string> All = typeof(SystemRoles).GetConstants();
    }
}
