using System.Windows;
using System.Threading.Tasks;
using System.Text;

using Globeport.Shared.Library.Xaml;

namespace Globeport.Shared.Library.Xaml.UWP
{
    public partial class Renderer
    {
        public string GetTemplate(AppBar element, string dataContext)
        {
            var sb = new StringBuilder();
            sb.Append($"<AppBar{GetTemplateBindings(element, dataContext)}>");
            if (element.Content is FrameworkElement)
            {
                sb.Append(GetTemplate((dynamic)element.Content, "Content"));
            }
            if (element.ContextFlyout?.Content != null)
            {
                sb.Append("<AppBar.ContextFlyout>");
                sb.Append($"<Flyout>");
                sb.Append(GetTemplate((dynamic)element.ContextFlyout.Content, null));
                sb.Append("</Flyout>");
                sb.Append("</AppBar.ContextFlyout>");
            }
            sb.Append("</AppBar>");
            return sb.ToString();
        }

        public string GetTemplateBindings(AppBar element, string dataContext)
        {
            var sb = new StringBuilder();
            sb.Append(GetTemplateBindings((ContentControl)element, dataContext));
            sb.Append(" binders:AppBarBinder.Element=\"{Binding Mode=OneTime}\"");
            return sb.ToString();
        }
    }
}
