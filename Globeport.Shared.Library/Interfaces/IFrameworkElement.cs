using System.Collections.Generic;
using System.Threading.Tasks;

using Globeport.Shared.Library.Interop;

namespace Globeport.Shared.Library.Interfaces
{
    public interface IFrameworkElement : IDependencyObject
    {
        [JsInterop]
        string Loaded { get; }
        [JsInterop]
        string SizeChanged { get; }
        [JsInterop]
        double ActualHeight { get; set; }
        [JsInterop]
        double ActualWidth { get; set; }
        [JsInterop]
        double Height { get; set; }
        [JsInterop]
        string HorizontalAlignment { get; set; }
        [JsInterop]
        string Margin { get; set; }
        [JsInterop]
        double MaxHeight { get; set; }
        [JsInterop]
        double MaxWidth { get; set; }
        [JsInterop]
        double MinHeight { get; set; }
        [JsInterop]
        double MinWidth { get; set; }
        [JsInterop]
        string RequestedTheme { get; set; }
        [JsInterop]
        string VerticalAlignment { get; set; }
        [JsInterop]
        double Width { get; set; }
    }
}