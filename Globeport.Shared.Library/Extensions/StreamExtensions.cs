using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Globeport.Shared.Library.Extensions
{
    public static class StreamExtensions
    {
        public static bool IsNullOrEmpty(this Stream stream)
        {
            return stream == null || stream.Length == 0;
        }
    }
}
