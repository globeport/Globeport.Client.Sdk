using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Globeport.Shared.Library.Extensions
{
    public static class TypeExtensions
    {
        public static T GetAttribute<T>(this Type type) where T : Attribute
        {
            return type.GetTypeInfo().GetCustomAttribute<T>();
        }
        public static T GetAttribute<T>(this PropertyInfo info) where T : Attribute
        {
            return info.GetCustomAttribute<T>();
        }
    }
}
