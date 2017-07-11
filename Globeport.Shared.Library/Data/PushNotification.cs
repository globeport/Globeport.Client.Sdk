using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Globeport.Shared.Library.Data
{
    public class PushNotification
    {
        public string Type { get; set; }
        public PropertyBag Data { get; set; }

        public PushNotification(string type, PropertyBag data = null)
        {
            Type = type;
            Data = data;
        }
    }
}
