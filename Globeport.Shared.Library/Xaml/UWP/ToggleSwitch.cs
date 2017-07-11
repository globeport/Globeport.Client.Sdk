using System.Windows;
using System.Threading.Tasks;
using System.Text;

using Globeport.Shared.Library.Xaml;

namespace Globeport.Shared.Library.Xaml.UWP
{
    public partial class Renderer
    {
        public string GetTemplate(ToggleSwitch element, string dataContext)
        {
            var sb = new StringBuilder();
            sb.Append($"<ToggleSwitch{GetTemplateBindings(element, dataContext)}>");
            if (element.Background is ImageBrush)
            {
                sb.Append("<ToggleSwitch.Background>");
                sb.Append(GetTemplate((ImageBrush)element.Background, "Background"));
                sb.Append("</ToggleSwitch.Background>");
            }
            if (element.ContextFlyout?.Content != null)
            {
                sb.Append("<ToggleSwitch.ContextFlyout>");
                sb.Append($"<Flyout>");
                sb.Append(GetTemplate((dynamic)element.ContextFlyout.Content, null));
                sb.Append("</Flyout>");
                sb.Append("</ToggleSwitch.ContextFlyout>");
            }
            sb.Append($"</ToggleSwitch>");
            return sb.ToString();
        }

        public string GetTemplateBindings(ToggleSwitch element, string dataContext)
        {
            var sb = new StringBuilder();
            sb.Append(GetTemplateBindings((Control)element, dataContext));
            sb.Append(" binders:ToggleSwitchBinder.Element=\"{Binding Mode=OneTime}\"");
            return sb.ToString();
        }
    }
}
