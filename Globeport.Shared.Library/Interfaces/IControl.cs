using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Globeport.Shared.Library.Interop;

namespace Globeport.Shared.Library.Interfaces
{
    public interface IControl
    {
        [JsInterop]
        void Focus();
        [JsInterop]
        string Foreground { get; set; }
        [JsInterop]
        object Background { get; set; }
        [JsInterop]
        string BorderThickness { get; set; }
        [JsInterop]
        string BorderBrush { get; set; }
        [JsInterop]
        string FontFamily { get; set; }
        [JsInterop]
        double FontSize { get; set; }
        [JsInterop]
        string FontStyle { get; set; }
        [JsInterop]
        string FontWeight { get; set; }
        [JsInterop]
        string HorizontalContentAlignment { get; set; }
        [JsInterop]
        string VerticalContentAlignment { get; set; }
        [JsInterop]
        string Padding { get; set; }
        [JsInterop]
        bool IsEnabled { get; set; }
    }
}
