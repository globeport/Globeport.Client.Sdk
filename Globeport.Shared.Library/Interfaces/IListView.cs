using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Globeport.Shared.Library.Interop;

namespace Globeport.Shared.Library.Interfaces
{
    public interface IListView
    {
        [JsInterop]
        object SelectedItems { get; }
        [JsInterop]
        object Header { get; }
        [JsInterop]
        string SelectionMode { get; set; }
        [JsInterop]
        bool IsItemClickEnabled { get; set; }
    }
}
