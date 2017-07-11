using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Globeport.Shared.Library.Interop;

namespace Globeport.Shared.Library.Interfaces
{
    public interface IAccount
    {
        [JsInterop]
        string Id { get; }
        [JsInterop]
        string Username { get;  }
        [JsInterop]
        string Name { get; set; }
        [JsInterop]
        string Biography { get; set; }
        [JsInterop]
        string ImageId { get; set; }
        [JsInterop]
        string Color { get; set; }
        [JsInterop]
        DateTimeOffset Created { get; set; }
        [JsInterop]
        DateTimeOffset Updated { get; set; }
        [JsInterop]
        DateTimeOffset Timestamp { get; set; }
    }
}
