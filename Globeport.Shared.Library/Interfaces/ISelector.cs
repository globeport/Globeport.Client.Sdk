using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Globeport.Shared.Library.Interop;

namespace Globeport.Shared.Library.Interfaces
{
    public interface ISelector
    {
        [JsInterop]
        string SelectionChanged { get; }
        [JsInterop]
        object SelectedItem { get; }
        [JsInterop]
        long SelectedIndex { get; set; }
        [JsInterop]
        object SelectedValue { get; set; }
        [JsInterop]
        string SelectedValuePath { get; set; }
    }
}
