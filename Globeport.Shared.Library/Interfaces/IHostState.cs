using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Globeport.Shared.Library.Interop;
using Globeport.Shared.Library.ClientModel;

namespace Globeport.Shared.Library.Interfaces
{
    public interface IHostState
    {
        [JsInterop]
        Entity Entity { get; }

        [JsInterop]
        Account Account { get; }
    }
}
