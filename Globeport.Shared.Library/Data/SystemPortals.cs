using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Globeport.Shared.Library.Extensions;

namespace Globeport.Shared.Library.Data
{
    public class SystemPortals
    {
        public const string Profile = nameof(Profile);
        public const string Public = nameof(Public);
        public const string Contacts = nameof(Contacts);
        public const string Friends = nameof(Friends);
        public const string Family = nameof(Family);
        public const string Work = nameof(Work);

        public static Dictionary<string,string> All = typeof(SystemPortals).GetConstants();

        public static Dictionary<string, string> Lists = All.Where(i=>!i.Value.In(Profile, Public)).ToDictionary(i=>i.Key, i=>i.Value);
    }
}
