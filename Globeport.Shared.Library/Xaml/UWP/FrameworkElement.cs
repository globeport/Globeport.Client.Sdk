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
        public string GetTemplateBindings(FrameworkElement element, string dataContext)
        {
            var sb = new StringBuilder();
            sb.Append(GetTemplateBindings((UIElement)element));
            if (dataContext != null) dataContext += ",";
            sb.Append($" DataContext=\"{{Binding {dataContext} Mode=OneTime}}\"");
            sb.Append(" binders:FrameworkElementBinder.Element=\"{Binding Mode=OneTime}\"");
            return sb.ToString();
        }
    }
}
