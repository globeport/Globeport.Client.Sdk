using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Newtonsoft.Json;

using Globeport.Shared.Library.Components;

namespace Globeport.Shared.Library.Interop
{
    public static class JsInterop
    {
        public static JsonSerializerSettings JsonSettings { get; } = new JsonSerializerSettings
        {
            NullValueHandling = NullValueHandling.Ignore,
            TypeNameHandling = TypeNameHandling.None,
            DateParseHandling = DateParseHandling.None,
            ContractResolver = new JsInteropContractResolver(),
            Converters = new[] { new DataObjectConverter() }
        };
    }
}
