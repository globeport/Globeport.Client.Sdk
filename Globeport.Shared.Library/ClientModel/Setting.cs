using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Globeport.Shared.Library.Extensions;

namespace Globeport.Shared.Library.ClientModel
{
    public class Setting : ClientObject
    {
        public string Value { get; set; }

        public Setting()
        {
        }

        public Setting(string id, string value)
            : base(id)
        {
            Value = value;
        }

        public T GetValue<T>()
        {
            return Value.ToType<T>();
        }
    }
}
