using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Globeport.Shared.Library.Interop;

namespace Globeport.Shared.Library.Interfaces
{
    public interface IScriptHost : IDisposable
    {
        event EventHandler<CallbackEventArgs> CallbackExecuted;
        object CallFunction(string name, params object[] parameters);
        object RunScript(string script);
        void SetProperty(string name, object value);
        object GetProperty(string name);
    }
}
