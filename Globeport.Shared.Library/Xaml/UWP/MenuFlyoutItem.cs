using System.Windows;
using System.Threading.Tasks;
using System.Text;

using Globeport.Shared.Library.Xaml;

namespace Globeport.Shared.Library.Xaml.UWP
{
    public partial class Renderer
    {
        public string GetTemplate(MenuFlyoutItem element, string dataContext)
        {
            var sb = new StringBuilder();
            sb.Append($"<MenuFlyoutItem{GetTemplateBindings(element, dataContext)}>");
            if (element.Background is ImageBrush)
            {
                sb.Append("<MenuFlyoutItem.Background>");
                sb.Append(GetTemplate((ImageBrush)element.Background, "Background"));
                sb.Append("</MenuFlyoutItem.Background>");
            }
            if (element.ContextFlyout?.Content != null)
            {
                sb.Append("<MenuFlyoutItem.ContextFlyout>");
                sb.Append($"<Flyout>");
                sb.Append(GetTemplate((dynamic)element.ContextFlyout.Content, null));
                sb.Append("</Flyout>");
                sb.Append("</MenuFlyoutItem.ContextFlyout>");
            }
            sb.Append($"</MenuFlyoutItem>");
            return sb.ToString();
        }

        public string GetTemplateBindings(MenuFlyoutItem element, string dataContext)
        {
            var sb = new StringBuilder();
            sb.Append(GetTemplateBindings((Control)element, dataContext));
            sb.Append(" binders:MenuFlyoutItemBinder.Element=\"{Binding Mode=OneTime}\"");
            return sb.ToString();
        }
    }
}
