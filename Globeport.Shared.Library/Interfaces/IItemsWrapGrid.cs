using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Globeport.Shared.Library.Interop;

namespace Globeport.Shared.Library.Interfaces
{
    public interface IItemsWrapGrid
    {
        [JsInterop]
        int MaximumRowsOrColumns { get; set; }
        [JsInterop]
        string Orientation { get; set; }
    }
}
