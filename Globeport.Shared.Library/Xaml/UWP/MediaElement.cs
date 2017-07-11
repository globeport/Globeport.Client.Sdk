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
        public string GetTemplate(MediaElement element, string dataContext)
        {
            var sb = new StringBuilder();
            sb.Append($"<MediaElement{GetTemplateBindings(element, dataContext)}>");
            if (element.ContextFlyout?.Content != null)
            {
                sb.Append("<ListView.ContextFlyout>");
                sb.Append($"<Flyout>");
                sb.Append(GetTemplate((dynamic)element.ContextFlyout.Content, null));
                sb.Append("</Flyout>");
                sb.Append("</ListView.ContextFlyout>");
            }
            sb.Append($"</MediaElement>");
            return sb.ToString();
        }

        public string GetTemplateBindings(MediaElement element, string dataContext)
        {
            var sb = new StringBuilder();
            sb.Append(GetTemplateBindings((FrameworkElement)element, dataContext));
            sb.Append(" binders:MediaElementBinder.Element=\"{Binding Mode=OneTime}\"");
            return sb.ToString();
        }
    }
}
