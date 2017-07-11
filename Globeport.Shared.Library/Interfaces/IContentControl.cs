using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Globeport.Shared.Library.Interop;

namespace Globeport.Shared.Library.Interfaces
{
    public interface IContentControl
    {
        [JsInterop]
        object Content { get; set; }
        [JsInterop]
        string Control { get; set; }
        [JsInterop]
        string Element { get; set; }
    }
}
