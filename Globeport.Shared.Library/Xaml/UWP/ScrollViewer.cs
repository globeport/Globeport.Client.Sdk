using System.Windows;
using System.Threading.Tasks;
using System.Text;

using Globeport.Shared.Library.Xaml;

namespace Globeport.Shared.Library.Xaml.UWP
{
    public partial class Renderer
    {
        public string GetTemplate(ScrollViewer element, string dataContext)
        {
            var sb = new StringBuilder();
            sb.Append($"<ScrollViewer{GetTemplateBindings(element, dataContext)}>");
            if (element.Content is FrameworkElement)
            {
                sb.Append(GetTemplate((dynamic)element.Content, "Content"));
            }
            if (element.ContextFlyout?.Content != null)
            {
                sb.Append("<ScrollViewer.ContextFlyout>");
                sb.Append($"<Flyout>");
                sb.Append(GetTemplate((dynamic)element.ContextFlyout.Content, null));
                sb.Append("</Flyout>");
                sb.Append("</ScrollViewer.ContextFlyout>");
            }
            sb.Append("</ScrollViewer>");
            return sb.ToString();
        }
    }
}
