using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Globeport.Shared.Library.Interop;

namespace Globeport.Shared.Library.Interfaces
{
    public interface IInkCanvas
    {
        [JsInterop]
        bool IsInputEnabled { get; set; }
        [JsInterop]
        string Color { get; set; }
        [JsInterop]
        string PenSize { get; set; }
    }
}
