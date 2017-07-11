using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Runtime.Serialization;
using System.Collections.Concurrent;

using Globeport.Shared.Library.Attributes;

namespace Globeport.Shared.Library.ClientModel
{
    public class ClientObject
    {
        public string Id { get; set; }

        public ClientObject()
        {
        }

        public ClientObject(string id)
        {
            Id = id;
        }

        public virtual DateTimeOffset GetTimestamp()
        {
            throw new NotImplementedException();
        }

        static Dictionary<string, PropertyInfo> GetProperties(Type type)
        {
            return type.GetRuntimeProperties().ToDictionary(i=>i.Name);
        }

        public T GetPropertyValue<T>(string propertyName)
        {
            var property = GetProperties(this.GetType()).Values.Where(p => p.Name == propertyName).FirstOrDefault();
            return (T) property.GetValue(this);
        }

        public IEnumerable<PropertyInfo> GetProperties()
        {
            return GetProperties(this.GetType()).Values.Where(p => !p.GetCustomAttributes<IgnoreAttribute>().Any());
        }

        public virtual object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
