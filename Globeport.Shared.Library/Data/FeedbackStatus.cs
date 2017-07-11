using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Globeport.Shared.Library.Data
{
    public static class FeedbackState
    {
        public const string Pending = nameof(Pending);
        public const string Reviewing = nameof(Reviewing);
        public const string Developing = nameof(Developing);
        public const string Testing = nameof(Testing);
        public const string Rejected = nameof(Rejected);
        public const string Completed = nameof(Completed);
    }
}
