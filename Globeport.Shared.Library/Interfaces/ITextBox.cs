using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Globeport.Shared.Library.Interop;

using Globeport.Shared.Library.Xaml;

namespace Globeport.Shared.Library.Interfaces
{
    public interface ITextBox
    {
        [JsInterop]
        string TextChanged { get; }
        [JsInterop]
        bool AcceptsReturn { get; set; }
        [JsInterop]
        string InputScope { get; set; }
        [JsInterop]
        bool IsColorFontEnabled { get; set; }
        [JsInterop]
        int MaxLength { get; set; }
        [JsInterop]
        bool IsReadOnly { get; set; }
        [JsInterop]
        bool IsSpellCheckEnabled { get; set; }
        [JsInterop]
        bool IsTextPredictionEnabled { get; set; }
        [JsInterop]
        string Text { get; set; }
    }
}
