using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

using Newtonsoft.Json;

using Globeport.Shared.Library.Components;

namespace Globeport.Shared.Library.Extensions
{
    public static class SerializeExtensions
    {
        public static JsonSerializerSettings NoTypeHandling = new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.None,
            NullValueHandling = NullValueHandling.Include,
            DateParseHandling = DateParseHandling.None,
            Converters = new[] { new DataObjectConverter() }
        };

        public static JsonSerializerSettings AutoTypeHandling = new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.Auto,
            NullValueHandling = NullValueHandling.Include,
            DateParseHandling = DateParseHandling.None,
            Converters = new[] { new DataObjectConverter() }
        };

        public static string Serialize<T>(this T obj, JsonSerializerSettings settings = null)
        {
            if (obj == null) return null;
            if (obj is string) return obj as string;
            return JsonConvert.SerializeObject(obj, settings ?? NoTypeHandling);
        }

        public static T Deserialize<T>(this string obj, JsonSerializerSettings settings = null)
        {
            if (obj == null) return default(T);
            return JsonConvert.DeserializeObject<T>(obj, settings ?? NoTypeHandling);
        }

        public static object Deserialize(this string obj, JsonSerializerSettings settings = null)
        {
            if (obj == null) return null;
            return JsonConvert.DeserializeObject(obj, settings ?? NoTypeHandling);
        }

        public static object Deserialize(this string obj, Type type, JsonSerializerSettings settings = null)
        {
            if (obj == null) return null;
            return JsonConvert.DeserializeObject(obj, type, settings ?? NoTypeHandling);
        }

        public static byte[] FromBase64(this string source)
        {
            if (source == null) return null;
            return Convert.FromBase64String(source);
        }

        public static string ToBase64(this byte[] source)
        {
            if (source == null) return null;
            return Convert.ToBase64String(source);
        }
    }
}
