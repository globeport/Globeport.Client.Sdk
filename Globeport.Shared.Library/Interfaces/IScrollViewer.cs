using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Globeport.Shared.Library.Interop;

namespace Globeport.Shared.Library.Interfaces
{
    public interface IScrollViewer
    {
        [JsInterop]
        string HorizontalScrollBarVisibility { get; set; }
        [JsInterop]
        string HorizontalScrollMode { get; set; }
        [JsInterop]
        string VerticalScrollBarVisibility { get; set; }
        [JsInterop]
        string VerticalScrollMode { get; set; }
    }
}
