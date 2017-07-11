using System;
using System.Threading.Tasks;
using System.Collections.Specialized;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

using Globeport.Shared.Library.Xaml;

namespace Globeport.Shared.Library.Xaml.UWP
{
    public partial class Renderer
    {
        public string GetTemplate(InkToolbar element, string dataContext)
        {
            var sb = new StringBuilder();
            sb.Append($"<InkToolbar{GetTemplateBindings(element, dataContext)}>");
            if (element.ContextFlyout?.Content != null)
            {
                sb.Append("<InkToolbar.ContextFlyout>");
                sb.Append($"<Flyout>");
                sb.Append(GetTemplate((dynamic)element.ContextFlyout.Content, null));
                sb.Append("</Flyout>");
                sb.Append("</InkToolbar.ContextFlyout>");
            }
            sb.Append($"</InkToolbar>");
            return sb.ToString();
        }

        public string GetTemplateBindings(InkToolbar element, string dataContext)
        {
            var sb = new StringBuilder();
            sb.Append(GetTemplateBindings((Control)element, dataContext));
            sb.Append(" binders:InkToolbarBinder.Element=\"{Binding Mode=OneTime}\"");
            return sb.ToString();
        }
    }
}
