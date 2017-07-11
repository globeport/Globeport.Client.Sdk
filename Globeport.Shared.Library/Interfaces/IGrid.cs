using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Globeport.Shared.Library.Interop;

using Globeport.Shared.Library.Xaml;

namespace Globeport.Shared.Library.Interfaces
{
    public interface IGrid
    {
        [JsInterop]
        ColumnDefinitions ColumnDefinitions { get;  }
        [JsInterop]
        RowDefinitions RowDefinitions { get; }
        [JsInterop]
        string BorderThickness { get; set; }
        [JsInterop]
        string BorderBrush { get; set; }
        [JsInterop]
        string CornerRadius { get; set; }
        [JsInterop]
        string Padding { get; set; }
    }
}
