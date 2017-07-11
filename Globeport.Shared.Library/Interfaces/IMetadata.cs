using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Globeport.Shared.Library.Interop;
using Globeport.Shared.Library.Components;

namespace Globeport.Shared.Library.Interfaces
{
    public interface IMetadata
    {
        [JsInterop]
        DataObject Global { get; }
        [JsInterop]
        DataObject Personal { get; }
    }
}
