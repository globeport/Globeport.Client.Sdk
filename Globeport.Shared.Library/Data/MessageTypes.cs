using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Globeport.Shared.Library.Data
{
    public static class MessageTypes
    {
        public const string Information = nameof(Information);
        public const string Warning = nameof(Warning);
        public const string Error = nameof(Error);
        public const string Success = nameof(Success);
        public const string Question = nameof(Question);
        public const string Input = nameof(Input);
        public const string Toast = nameof(Toast);
    }
}
