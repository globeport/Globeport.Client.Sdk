using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Globeport.Shared.Library.Interop;

namespace Globeport.Shared.Library.Interfaces
{
    public interface IProgressBar
    {
        [JsInterop]
        bool IsIndeterminate { get; set; }
        [JsInterop]
        double Maximum { get; set; }
        [JsInterop]
        double Minimum { get; set; }
        [JsInterop]
        double Value { get; set; }
    }
}
