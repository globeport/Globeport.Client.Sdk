using System.Windows;
using System.Threading.Tasks;
using System.Text;

using Globeport.Shared.Library.Xaml;

namespace Globeport.Shared.Library.Xaml.UWP
{
    public partial class Renderer
    {
        public string GetTemplate(ContentControl element, string dataContext)
        {
            var sb = new StringBuilder();
            sb.Append($"<ContentControl{GetTemplateBindings(element, dataContext)}>");
            if (element.Background is ImageBrush)
            {
                sb.Append("<ContentControl.Background>");
                sb.Append(GetTemplate((ImageBrush)element.Background, "Background"));
                sb.Append("</ContentControl.Background>");
            }
            if (element.Content is FrameworkElement)
            {
                sb.Append(GetTemplate((dynamic)element.Content, "Content"));
            }
            if (element.ContextFlyout?.Content != null)
            {
                sb.Append("<ContentControl.ContextFlyout>");
                sb.Append($"<Flyout>");
                sb.Append(GetTemplate((dynamic)element.ContextFlyout.Content, null));
                sb.Append("</Flyout>");
                sb.Append("</ContentControl.ContextFlyout>");
            }
            sb.Append("</ContentControl>");
            return sb.ToString();
        }

        public string GetTemplateBindings(ContentControl element, string dataContext)
        {
            var sb = new StringBuilder();
            sb.Append(GetTemplateBindings((Control)element, dataContext));
            sb.Append(" binders:ContentControlBinder.Element=\"{Binding Mode=OneTime}\"");
            return sb.ToString();
        }
    }
}
