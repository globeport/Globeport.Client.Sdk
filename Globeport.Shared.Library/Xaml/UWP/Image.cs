using System;
using System.Windows;
using System.Threading.Tasks;
using System.Text;
using System.Linq;

using Globeport.Shared.Library.Xaml;

namespace Globeport.Shared.Library.Xaml.UWP
{
    public partial class Renderer
    {
        public string GetTemplate(Image element, string dataContext)
        {
            var sb = new StringBuilder();
            sb.Append($"<Image{GetTemplateBindings(element, dataContext)}>");
            if (element.ContextFlyout?.Content != null)
            {
                sb.Append("<Image.ContextFlyout>");
                sb.Append($"<Flyout>");
                sb.Append(GetTemplate((dynamic)element.ContextFlyout.Content, null));
                sb.Append("</Flyout>");
                sb.Append("</Image.ContextFlyout>");
            }
            sb.Append($"</Image>");
            return sb.ToString();
        }

        public string GetTemplateBindings(Image element, string dataContext)
        {
            var sb = new StringBuilder();
            sb.Append(GetTemplateBindings((FrameworkElement)element, dataContext));
            sb.Append(" binders:ImageBinder.Element=\"{Binding Mode=OneTime}\"");
            return sb.ToString();
        }
    }
}
