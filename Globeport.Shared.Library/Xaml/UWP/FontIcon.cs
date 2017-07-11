using System.Windows;
using System.Threading.Tasks;
using System.Text;
using System.Linq;

using Globeport.Shared.Library.Xaml;

namespace Globeport.Shared.Library.Xaml.UWP
{
    public partial class Renderer
    {
        public string GetTemplate(FontIcon element, string dataContext)
        {
            var sb = new StringBuilder();
            sb.Append($"<FontIcon{GetTemplateBindings(element, dataContext)}>");
            if (element.ContextFlyout?.Content != null)
            {
                sb.Append("<FontIcon.ContextFlyout>");
                sb.Append($"<Flyout>");
                sb.Append(GetTemplate((dynamic)element.ContextFlyout.Content, null));
                sb.Append("</Flyout>");
                sb.Append("</FontIcon.ContextFlyout>");
            }
            sb.Append($"</FontIcon>");
            return sb.ToString();
        }

        public string GetTemplateBindings(FontIcon element, string dataContext)
        {
            var sb = new StringBuilder();
            sb.Append(GetTemplateBindings((Icon)element, dataContext));
            sb.Append(" binders:FontIconBinder.Element=\"{Binding Mode=OneTime}\"");
            return sb.ToString();
        }
    }
}
