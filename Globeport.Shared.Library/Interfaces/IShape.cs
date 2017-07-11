using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Globeport.Shared.Library.Interop;

namespace Globeport.Shared.Library.Interfaces
{
    public interface IShape
    {
        [JsInterop]
        object Fill { get; set; }
        [JsInterop]
        double StrokeThickness { get; set; }
        [JsInterop]
        string Stroke { get; set; }
        [JsInterop]
        string Stretch { get; set; }
    }
}
