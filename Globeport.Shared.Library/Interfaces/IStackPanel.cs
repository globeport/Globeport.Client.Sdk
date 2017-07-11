using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Globeport.Shared.Library.Interop;

namespace Globeport.Shared.Library.Interfaces
{
    public interface IStackPanel
    {
        [JsInterop]
        string Orientation { get; set; }
        [JsInterop]
        string CornerRadius { get; set; }
        [JsInterop]
        string Padding { get; set; }
        [JsInterop]
        string BorderThickness { get; set; }
        [JsInterop]
        string BorderBrush { get; set; }
    }
}
