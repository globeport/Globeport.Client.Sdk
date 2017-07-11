using System.Windows;
using System.Threading.Tasks;
using System.Threading;
using System.Text;

using Globeport.Shared.Library.Xaml;

namespace Globeport.Shared.Library.Xaml.UWP
{
    public partial class Renderer
    {
        public string GetTemplate(HyperlinkButton element, string dataContext)
        {
            var sb = new StringBuilder();
            sb.Append($"<HyperlinkButton{GetTemplateBindings(element, dataContext)}>");
            if (element.Content is FrameworkElement)
            {
                sb.Append(GetTemplate((dynamic)element.Content, "Content"));
            }
            if (element.Flyout != null)
            {
                sb.Append("<Button.Flyout>");
                sb.Append($"<Flyout>");
                sb.Append(GetTemplate((dynamic)element.Flyout.Content, null));
                sb.Append("</Flyout>");
                sb.Append("</Button.Flyout>");
            }
            if (element.ContextFlyout?.Content != null)
            {
                sb.Append("<Button.ContextFlyout>");
                sb.Append($"<Flyout>");
                sb.Append(GetTemplate((dynamic)element.ContextFlyout.Content, null));
                sb.Append("</Flyout>");
                sb.Append("</Button.ContextFlyout>");
            }
            sb.Append("</HyperlinkButton>");
            return sb.ToString();
        }

        public string GetTemplateBindings(HyperlinkButton element, string dataContext)
        {
            var sb = new StringBuilder();
            sb.Append(GetTemplateBindings((Button)element, dataContext));
            sb.Append(" binders:HyperlinkButtonBinder.Element=\"{Binding Mode=OneTime}\"");
            return sb.ToString();
        }
    }
}
