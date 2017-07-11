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
        public string GetTemplateBindings(Selector element, string dataContext)
        {
            var sb = new StringBuilder();
            sb.Append(GetTemplateBindings((ItemsControl)element, dataContext));
            sb.Append(" binders:SelectorBinder.Element=\"{Binding Mode=OneTime}\"");
            return sb.ToString();
        }
    }
}
