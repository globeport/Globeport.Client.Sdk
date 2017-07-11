using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Globeport.Shared.Library.Interop;

namespace Globeport.Shared.Library.Interfaces
{
    public interface IBinding
    {
        [JsInterop]
        string Path { get; }
        [JsInterop]
        string Mode { get; }
        [JsInterop]
        string Converter { get; }
        [JsInterop]
        string ConverterParameter { get; }
        [JsInterop]
        string PropertyName { get; }
        [JsInterop]
        string TargetNullValue { get; }
    }
}
