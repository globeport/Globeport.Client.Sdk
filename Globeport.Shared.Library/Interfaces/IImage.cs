using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Globeport.Shared.Library.Interop;

namespace Globeport.Shared.Library.Interfaces
{
    public interface IImage
    {
        [JsInterop]
        string Source { get; set; }
        [JsInterop]
        int Size { get; set; }
        [JsInterop]
        string Stretch { get; set; }
    }
}
