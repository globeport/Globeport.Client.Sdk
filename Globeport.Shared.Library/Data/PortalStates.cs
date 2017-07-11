using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Globeport.Shared.Library.Data
{
    public static class PortalStates
    {
        public const string Disconnected = nameof(Disconnected);
        public const string Connected = nameof(Connected);
        public const string Added = nameof(Added);
        public const string Left = nameof(Left);
        public const string Removed = nameof(Removed);
        public const string Deleted = nameof(Deleted);
    }
}
