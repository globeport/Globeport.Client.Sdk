using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Globeport.Shared.Library.Extensions
{
    public static class DateTimeOffsetExtensions
    {
        public static DateTimeOffset Truncate(this DateTimeOffset date, long resolution)
        {
            return new DateTimeOffset(date.Ticks - (date.Ticks % resolution), TimeSpan.Zero);
        }
    }
}
