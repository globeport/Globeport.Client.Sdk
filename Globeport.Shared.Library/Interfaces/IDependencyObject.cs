using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

using Globeport.Shared.Library.Interop;
using Globeport.Shared.Library.Xaml;

namespace Globeport.Shared.Library.Interfaces
{
    public interface IDependencyObject : INotifyPropertyChanged
    {
        [JsInterop]
        string PropertyUpdated { get; }
        IXamlHost Host { get; }
        [JsInterop]
        Bindings Bindings { get; }
        [JsInterop]
        string Name { get; set; }
        [JsInterop]
        object DataContext { get; set; }
        [JsInterop]
        object Tag { get; set; }
        [JsInterop]
        string Type { get; }
        [JsInterop]
        int Id { get; set; }
    }
}
