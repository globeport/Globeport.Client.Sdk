using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Globeport.Shared.Library.Xaml;

namespace Globeport.Shared.Library.Xaml.UWP
{
    public partial class Renderer
    {
        public string GetTemplateBindings(UIElement element)
        {
            var sb = new StringBuilder();
            sb.Append(" binders:UIElementBinder.Element=\"{Binding Mode=OneTime}\"");
            return sb.ToString();
        }
    }
}
