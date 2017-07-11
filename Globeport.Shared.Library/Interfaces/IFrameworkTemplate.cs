using System;
using System.Collections.Generic;
using System.Text;

using Globeport.Shared.Library.Xaml;
using Globeport.Shared.Library.Interop;

namespace Globeport.Shared.Library.Interfaces
{
    public interface IFrameworkTemplate
    {
        [JsInterop]
        FrameworkElement Template { get; }
    }
}
