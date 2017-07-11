using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Globeport.Shared.Library.Extensions;

namespace Globeport.Shared.Library.Data
{
    public class ImageSizes
    {
        public static int[] All = new[] { 32, 64, 128, 256, 512, 1024 };
        public static int Min = All.Min();
        public static int Max = All.Max();
    }
}
