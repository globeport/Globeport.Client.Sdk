using System.Windows;
using System.Threading.Tasks;
using System.Text;

using Globeport.Shared.Library.Xaml;

namespace Globeport.Shared.Library.Xaml.UWP
{
    public partial class Renderer
    {
        public string GetTemplate(RadioButton element, string dataContext)
        {
            var sb = new StringBuilder();
            sb.Append($"<RadioButton{GetTemplateBindings(element, dataContext)}>");
            if (element.Content is FrameworkElement)
            {
                sb.Append(GetTemplate((dynamic)element.Content, "Content"));
            }
            if (element.ContextFlyout?.Content != null)
            {
                sb.Append("<RadioButton.ContextFlyout>");
                sb.Append($"<Flyout>");
                sb.Append(GetTemplate((dynamic)element.ContextFlyout.Content, null));
                sb.Append("</Flyout>");
                sb.Append("</RadioButton.ContextFlyout>");
            }
            sb.Append($"</RadioButton>");
            return sb.ToString();
        }

        public string GetTemplateBindings(RadioButton element, string dataContext)
        {
            var sb = new StringBuilder();
            sb.Append(GetTemplateBindings((ToggleButton)element, dataContext));
            sb.Append(" binders:RadioButtonBinder.Element=\"{Binding Mode=OneTime}\"");
            return sb.ToString();
        }
    }
}
