using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Globeport.Shared.Library.Interop;

namespace Globeport.Shared.Library.Interfaces
{
    public interface ITextBlock
    {
        [JsInterop]
        string Text { get; set; }
        [JsInterop]
        string FontFamily { get; set; }
        [JsInterop]
        double FontSize { get; set; }
        [JsInterop]
        string FontStyle { get; set; }
        [JsInterop]
        string FontWeight { get; set; }
        [JsInterop]
        string Foreground { get; set; }
        [JsInterop]
        bool IsColorFontEnabled { get; set; }
        [JsInterop]
        string Padding { get; set; }
        [JsInterop]
        string MentionsUrl { get; set; }
        [JsInterop]
        string TextWrapping { get; set; }
        [JsInterop]
        string TextAlignment { get; set; }
    }
}
