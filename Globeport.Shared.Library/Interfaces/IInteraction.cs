using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Globeport.Shared.Library.Interop;

using Globeport.Shared.Library.ClientModel;
using Globeport.Shared.Library.Components;

namespace Globeport.Shared.Library.Interfaces
{
    public interface IInteraction
    {
        [JsInterop]
        string Id { get; }
        [JsInterop]
        string Type { get; }
        [JsInterop]
        string Tag { get; }
        [JsInterop]
        string EntityId { get; }
        [JsInterop]
        string AccountId { get; }
        [JsInterop]
        DataObject Properties { get; }
        [JsInterop]
        Avatar Avatar { get; }
        [JsInterop]
        DateTimeOffset Created { get; }
        [JsInterop]
        DateTimeOffset Updated { get; }
        [JsInterop]
        DateTimeOffset Timestamp { get; }
    }
}
