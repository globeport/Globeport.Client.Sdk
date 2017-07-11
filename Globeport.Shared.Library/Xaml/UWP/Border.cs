using System.Windows;
using System.Threading.Tasks;
using System.Text;
using System.Linq;

using Globeport.Shared.Library.Xaml;

namespace Globeport.Shared.Library.Xaml.UWP
{
    public partial class Renderer
    {
        public string GetTemplate(Border element, string dataContext)
        {
            var sb = new StringBuilder();
            sb.Append($"<Border{GetTemplateBindings(element, dataContext)}>");
            if (element.Child is FrameworkElement)
            {
                sb.Append(GetTemplate((dynamic)element.Child, "Child"));       
            }
            if (element.ContextFlyout?.Content != null)
            {
                sb.Append("<Border.ContextFlyout>");
                sb.Append($"<Flyout>");
                sb.Append(GetTemplate((dynamic)element.ContextFlyout.Content, null));
                sb.Append("</Flyout>");
                sb.Append("</Border.ContextFlyout>");
            }
            sb.Append("</Border>");
            return sb.ToString();
        }

        public string GetTemplateBindings(Border element, string dataContext)
        {
            var sb = new StringBuilder();
            sb.Append(GetTemplateBindings((FrameworkElement)element, dataContext));
            sb.Append(" binders:BorderBinder.Element=\"{Binding Mode=OneTime}\"");
            return sb.ToString();
        }
    }
}
