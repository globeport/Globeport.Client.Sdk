using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Globeport.Shared.Library.Collections;

namespace Globeport.Shared.Library.Xaml
{
    public class ItemsCollection : ObservableList<object>
    {
        public ItemsCollection()
        {
        }

        public ItemsCollection(IEnumerable<object> collection)
            : base(collection)
        {
        }

        public object[] ToArray()
        {
            return Enumerable.ToArray(this);
        }
    }
}