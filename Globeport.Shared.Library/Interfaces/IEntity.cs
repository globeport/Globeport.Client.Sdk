using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Globeport.Shared.Library.Interop;

namespace Globeport.Shared.Library.Interfaces
{
    public interface IEntity
    {
        [JsInterop]
        string Id { get; }
        [JsInterop]
        string AccountId { get; }
        [JsInterop]
        string ModelId { get; }
        [JsInterop]
        string ClassId { get; }
        [JsInterop]
        DateTimeOffset Created { get; }
        [JsInterop]
        DateTimeOffset Updated { get; }
        [JsInterop]
        DateTimeOffset Timestamp { get; }
    }
}
