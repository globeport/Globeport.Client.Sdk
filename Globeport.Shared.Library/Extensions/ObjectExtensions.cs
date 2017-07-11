using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace Globeport.Shared.Library.Extensions
{
    public static class ObjectExtensions
    {
        private static Dictionary<Type, object> defaults = new Dictionary<Type, object>() {
            { typeof(int), default(int) },
            { typeof(bool), default(bool) },
            { typeof(long), default(long) },
            { typeof(short), default(short) },
            { typeof(double), default(double) },
            { typeof(DateTimeOffset), default(DateTimeOffset) },
            { typeof(Guid), default(Guid) },
            { typeof(string), string.Empty }
        };

        static Dictionary<Type, Func<string, object>> parserFuncs = new Dictionary<Type, Func<string, object>>() {
            { typeof(int), i => int.Parse(i) },
            { typeof(bool), i => bool.Parse(i) },
            { typeof(long), i => long.Parse(i) },
            { typeof(short), i => short.Parse(i) },
            { typeof(double), i => double.Parse(i) },
            { typeof(DateTimeOffset), i => DateTimeOffset.Parse(i) },
            { typeof(Guid), i => Guid.Parse(i) }
        };

        public static object ToType(this string obj, Type type)
        {
            if (parserFuncs.ContainsKey(type))
            {
                return parserFuncs[type](obj);
            }
            return obj;
        }

        public static T ToType<T>(this string obj)
        {
            if (obj == null) return default(T);
            return (T)obj.ToType(typeof(T));
        }

        public static object GetDefault(this Type type)
        {
            if (defaults.ContainsKey(type))
            {
                return defaults[type];
            }
            return null;
        }

        public static T Max<T>(this T value1, T value2) where T : IComparable
        {
            return value1.CompareTo(value2) > 0 ? value1 : value2;
        }

        public static T Min<T>(this T value1, T value2) where T : IComparable
        {
            return value1.CompareTo(value2) < 0 ? value1 : value2;
        }

        public static List<PropertyInfo> GetProperties<T>(this object obj)
        {
            return obj.GetType().GetRuntimeProperties().Where(i => typeof(T).GetTypeInfo().IsAssignableFrom(i.PropertyType.GetTypeInfo())).ToList();
        }

        public static Dictionary<string, string> GetConstants(this Type type)
        {
            return type.GetTypeInfo().DeclaredFields.Where(i => i.IsLiteral && i.FieldType == typeof(string)).ToDictionary(i => i.Name, i => (string)i.GetValue(null));
        }

        public static PropertyInfo GetProperty(this Type type, string name)
        {
            return type.GetRuntimeProperties().FirstOrDefault(i => i.Name == name);
        }

        public static IEnumerable<PropertyInfo> GetProperties(this Type type, string[] names)
        {
            return type.GetRuntimeProperties().Where(i => names.Contains(i.Name));
        }

        public static T GetPropertyValue<T>(this object obj, string name)
        {
            return (T)obj.GetType().GetRuntimeProperties().FirstOrDefault(i => i.Name == name)?.GetValue(obj);
        }

        public static object GetPropertyValue(this object obj, string name)
        {
            return obj.GetType().GetRuntimeProperties().FirstOrDefault(i => i.Name == name)?.GetValue(obj);
        }

        public static void SetPropertyValue(this object obj, string name, object value)
        {
            var property = obj.GetType().GetRuntimeProperties().FirstOrDefault(i => i.Name == name);
            if (property != null)
            {
                if (property.PropertyType != typeof(string) && value is string)
                {
                    value = ((string)value).ToType(property.PropertyType);
                }
                if (property.PropertyType.GetTypeInfo().IsEnum)
                {
                    value = Convert.ToInt32(value);
                }
                property.SetValue(obj, value);
            }
        }

        public static bool In<T>(this T item, params T[] list)
        {
            return list.Contains(item);
        }
    }

}
