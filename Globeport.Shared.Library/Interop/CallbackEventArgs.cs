using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Globeport.Shared.Library.Interop
{
    public class CallbackEventArgs : EventArgs
    {
        public object Result { get; set; }
        public object[] Parameters { get; }

        public CallbackEventArgs(object[] parameters)
        {
            Parameters = parameters;
        }
    }
}
