using System.Windows;
using System.Threading.Tasks;
using System.Text;
using System.Linq;

using Globeport.Shared.Library.Xaml;

namespace Globeport.Shared.Library.Xaml.UWP
{
    public partial class Renderer
    {
        public string GetTemplate(Button element, string dataContext)
        {
            var sb = new StringBuilder();
            sb.Append($"<Button{GetTemplateBindings(element, dataContext)}>");
            if (element.Content is FrameworkElement)
            {
                sb.Append(GetTemplate((dynamic)element.Content, "Content"));
            }
            if (element.Flyout?.Content != null)
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
            sb.Append("</Button>");
            return sb.ToString();
        }

        public string GetTemplateBindings(Button element, string dataContext)
        {
            var sb = new StringBuilder();
            sb.Append(GetTemplateBindings((ContentControl)element, dataContext));
            sb.Append(" binders:ButtonBinder.Element=\"{Binding Mode=OneTime}\"");
            return sb.ToString();
        }
    }
}
