using System.Collections.Generic;
using System.Threading.Tasks;

using Globeport.Shared.Library.Interop;
using Globeport.Shared.Library.Xaml;

namespace Globeport.Shared.Library.Interfaces
{
    public interface IUIElement : IDependencyObject
    {
        [JsInterop]
        bool IsHitTestVisible { get; set; }
        [JsInterop]
        double Opacity { get; set; }
        [JsInterop]
        string Visibility { get; set; }
        [JsInterop]
        Flyout ContextFlyout { get; set; }
        [JsInterop]
        string Tapped { get; set; }
        [JsInterop]
        string RightTapped { get; set; }
    }
}