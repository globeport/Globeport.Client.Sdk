using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Globeport.Shared.Library.Interop;

namespace Globeport.Shared.Library.Interfaces
{
    public interface IAppBar
    {
        [JsInterop]
        string ClosedDisplayMode { get; set; }
    }
}
