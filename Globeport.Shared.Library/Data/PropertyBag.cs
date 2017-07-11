using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

using Globeport.Shared.Library.Extensions;

namespace Globeport.Shared.Library.Data
{
    public class PropertyBag : Dictionary<string, object>
    {
        public PropertyBag() : base()
        {
        }

        public PropertyBag(IDictionary<string, object> items)
            : base(items)
        {
        }

        public PropertyBag(string key, object value)
        {
            Add(key, value);
        }

        public new PropertyBag Add(string key, object value)
        {
            this.AddOrUpdate(key, value);
            return this;
        }

        public PropertyBag AddRange(PropertyBag items)
        {
            foreach (var item in items) this.Add(item.Key, item.Value);
            return this;
        }

        public new object this[string key]
        {
            get
            {
                if (ContainsKey(key)) return base[key];
                return null;
            }
            set
            {
                base[key] = value;
            }
        }

        public T GetValue<T>(string key)
        {
            if (ContainsKey(key)) return (T) this[key];
            return default(T);
        }
    }
}
