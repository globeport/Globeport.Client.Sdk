using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Globeport.Shared.Library.Extensions;

namespace Globeport.Shared.Library.Data
{
    public class SystemImages
    {
        public const string Profile = "2HHJDFDS0400";
        public const string Public = "2HHJDFK80400";
        public const string Contacts = "2HHJDFL00400";
        public const string Friends = "2HHJDFLK0400";
        public const string Family = "2HHJDFM40400";
        public const string Work = "2HHJDFMO0400";
        public const string Avatar = "2HHJDFN80400";
        public const string Script = "2HHJDFNO0400";
        public const string Table = "2HHJDFOC0400";
        public const string Xaml = "3OHOLFD40400";

        public static Dictionary<string, string> All = typeof(SystemImages).GetConstants();
    }
}
