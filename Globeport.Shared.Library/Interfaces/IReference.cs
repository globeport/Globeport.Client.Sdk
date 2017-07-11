using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Globeport.Shared.Library.Interop;

namespace Globeport.Shared.Library.Interfaces
{
    public interface IReference
    {
        [JsInterop]
        string Id { get; }
        [JsInterop]
        string Type { get; }
        [JsInterop]
        string Name { get; }
        [JsInterop]
        string FileId { get; }
        [JsInterop]
        bool IsDirect { get; }
    }
}
