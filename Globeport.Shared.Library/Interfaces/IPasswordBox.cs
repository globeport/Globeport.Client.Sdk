using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Globeport.Shared.Library.Interop;

namespace Globeport.Shared.Library.Interfaces
{
    public interface IPasswordBox
    {
        [JsInterop]
        string PasswordRevealMode { get; set; }
        [JsInterop]
        string Password { get; set; }
        [JsInterop]
        string InputScope { get; set; }
        [JsInterop]
        int MaxLength { get; set; }
    }
}
