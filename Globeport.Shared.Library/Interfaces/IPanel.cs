using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Globeport.Shared.Library.Interop;

using Globeport.Shared.Library.Xaml;

namespace Globeport.Shared.Library.Interfaces
{
    public interface IPanel
    {
        [JsInterop]
        object Background { get; }
        [JsInterop]
        FrameworkElements Children { get; }
    }
}
