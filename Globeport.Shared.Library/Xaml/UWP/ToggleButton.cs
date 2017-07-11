using System.Windows;
using System.Threading.Tasks;
using System.Text;

using Globeport.Shared.Library.Xaml;
using Globeport.Shared.Library.Extensions;

namespace Globeport.Shared.Library.Xaml.UWP
{
    public partial class Renderer
    {
        public string GetTemplate(ToggleButton element, string dataContext)
        {
            var sb = new StringBuilder();
            sb.Append($"<ToggleButton{GetTemplateBindings(element, dataContext)}>");
            if (element.Content is FrameworkElement)
            {
                sb.Append(GetTemplate((dynamic)element.Content, "Content"));
            }
            if (element.ContextFlyout?.Content != null)
            {
                sb.Append("<ToggleButton.ContextFlyout>");
                sb.Append($"<Flyout>");
                sb.Append(GetTemplate((dynamic)element.ContextFlyout.Content, null));
                sb.Append("</Flyout>");
                sb.Append("</ToggleButton.ContextFlyout>");
            }
            sb.Append($"</ToggleButton>");
            return sb.ToString();
        }

        public string GetTemplateBindings(ToggleButton element, string dataContext)
        {
            var sb = new StringBuilder();
            sb.Append(GetTemplateBindings((ContentControl)element, dataContext));
            sb.Append(" binders:ToggleButtonBinder.Element=\"{Binding Mode=OneTime}\"");
            return sb.ToString();
        }
    }
}
