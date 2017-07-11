using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Globeport.Shared.Library.Xaml
{
    public class MethodEventArgs : EventArgs
    {
        public string Method { get; set; }
        public object[] Parameters { get; }

        public MethodEventArgs(string method, params object[] parameters)
        {
            Method = method;
            Parameters = parameters;
        }
    }
}
