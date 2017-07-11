using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Globeport.Shared.Library.Interop;

namespace Globeport.Shared.Library.Interfaces
{
    public interface IDatePicker
    {
        [JsInterop]
        string Date { get; set; }
        [JsInterop]
        bool DayVisible { get; set; }
        [JsInterop]
        bool MonthVisible { get; set; }
        [JsInterop]
        bool YearVisible { get; set; }
    }
}
