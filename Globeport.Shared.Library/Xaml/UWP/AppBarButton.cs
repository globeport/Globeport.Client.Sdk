using System.Windows;
using System.Threading.Tasks;
using System.Text;

using Globeport.Shared.Library.Xaml;

namespace Globeport.Shared.Library.Xaml.UWP
{
    public partial class Renderer
    {
        public string GetTemplate(AppBarButton element, string dataContext)
        {
            var sb = new StringBuilder();
            sb.Append($"<AppBarButton{GetTemplateBindings(element, dataContext)}>");
            if (element.Content is FrameworkElement)
            {
                sb.Append(GetTemplate((dynamic)element.Content, "Content"));
            }
            if (element.Flyout != null)
            {
                sb.Append("<AppBarButton.Flyout>");
                sb.Append($"<Flyout>");
                sb.Append(GetTemplate((dynamic)element.Flyout.Content, null));
                sb.Append("</Flyout>");
                sb.Append("</AppBarButton.Flyout>");
            }
            if (element.ContextFlyout?.Content != null)
            {
                sb.Append("<AppBarButton.ContextFlyout>");
                sb.Append($"<Flyout>");
                sb.Append(GetTemplate((dynamic)element.ContextFlyout.Content, null));
                sb.Append("</Flyout>");
                sb.Append("</AppBarButton.ContextFlyout>");
            }
            sb.Append("</AppBarButton>");
            return sb.ToString();
        }
    }
}
