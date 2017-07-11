using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Globeport.Shared.Library.Extensions;

namespace Globeport.Shared.Library.Data
{
    public class SystemScripts
    {
        public const string EditMode = nameof(EditMode);
        public const string CardMode = nameof(CardMode);
        public const string FullMode = nameof(FullMode);
        public const string Class = nameof(Class);

        public const string Server = nameof(Server);

        public static Dictionary<string, string> All = typeof(SystemScripts).GetConstants();
    }
}
