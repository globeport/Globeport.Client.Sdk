using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Runtime.Serialization;

using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Globeport.Shared.Library.Interop
{
    public class JsInteropContractResolver : DefaultContractResolver
    {
        public JsInteropContractResolver()
        {
        }
        protected override IList<JsonProperty> CreateProperties(Type type, MemberSerialization memberSerialization)
        {
            var interfaces = type.GetTypeInfo().ImplementedInterfaces;

            var propertyNames = new HashSet<string>(interfaces.SelectMany(i => i.GetRuntimeProperties().Where(j => j.GetCustomAttributes(typeof(JsInteropAttribute), false).Any())).Select(i => i.Name));

            return base.CreateProperties(type, memberSerialization).Where(i => propertyNames.Contains(i.PropertyName)).ToList();
        }
    }
}
