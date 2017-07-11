using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Globeport.Shared.Library.Interop;
using Globeport.Shared.Library.ClientModel;

namespace Globeport.Shared.Library.Interfaces
{
    public interface IModel
    {
        [JsInterop]
        string Id { get; }
        [JsInterop]
        string AccountId { get; }
        [JsInterop]
        string ClassId { get; }
        [JsInterop]
        string Name { get; }
        [JsInterop]
        string Label { get;}
        [JsInterop]
        string ImageId { get; }
        [JsInterop]
        string Color { get; }
        [JsInterop]
        DateTimeOffset Created { get; }
        [JsInterop]
        DateTimeOffset Updated { get; }
        [JsInterop]
        DateTimeOffset Timestamp { get; }
    }
}
