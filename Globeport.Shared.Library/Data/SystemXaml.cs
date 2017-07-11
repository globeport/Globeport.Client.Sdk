using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Globeport.Shared.Library.Extensions;

namespace Globeport.Shared.Library.Data
{
    public class SystemXaml
    {
        public const string EditMode = nameof(EditMode);
        public const string CardMode = nameof(CardMode);
        public const string FullMode = nameof(FullMode);

        public static Dictionary<string, string> All = typeof(SystemXaml).GetConstants();
    }
}
