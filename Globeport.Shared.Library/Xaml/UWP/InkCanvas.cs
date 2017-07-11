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
        public string GetTemplate(InkCanvas element, string dataContext)
        {
            var sb = new StringBuilder();
            sb.Append($"<InkCanvas{GetTemplateBindings(element, dataContext)}>");
            if (element.ContextFlyout?.Content != null)
            {
                sb.Append("<InkCanvas.ContextFlyout>");
                sb.Append($"<Flyout>");
                sb.Append(GetTemplate((dynamic)element.ContextFlyout.Content, null));
                sb.Append("</Flyout>");
                sb.Append("</InkCanvas.ContextFlyout>");
            }
            sb.Append($"</InkCanvas>");
            return sb.ToString();
        }

        public string GetTemplateBindings(InkCanvas element, string dataContext)
        {
            var sb = new StringBuilder();
            sb.Append(GetTemplateBindings((FrameworkElement)element, dataContext));
            sb.Append(" binders:InkCanvasBinder.Element=\"{Binding Mode=OneTime}\"");
            return sb.ToString();
        }
    }
}
