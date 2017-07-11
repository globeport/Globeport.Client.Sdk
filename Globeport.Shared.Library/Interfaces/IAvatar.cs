using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Globeport.Shared.Library.Interop;

namespace Globeport.Shared.Library.Interfaces
{
    public interface IAvatar
    {
        [JsInterop]
        string Id { get; }
        [JsInterop]
        string AccountId { get; }
        [JsInterop]
        string Username { get; }
        [JsInterop]
        string Name { get; }
        [JsInterop]
        string Biography { get; }
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
