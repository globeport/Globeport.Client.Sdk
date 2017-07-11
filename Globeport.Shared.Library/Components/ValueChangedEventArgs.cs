using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Globeport.Shared.Library.Components
{
    public class ValueChangedEventArgs<T> : EventArgs
    {
        public T From { get; private set; }
        public T To { get; private set; }
        public ValueChangedEventArgs(T from, T to)
        {
            From = from;
            To = to;
        }
    }
}
