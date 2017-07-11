using System.Windows;
using System.Threading.Tasks;
using System.Text;
using System.Linq;

using Globeport.Shared.Library.Xaml;

namespace Globeport.Shared.Library.Xaml.UWP
{
    public partial class Renderer
    {
        public string GetTemplate(BitmapIcon element, string dataContext)
        {
            var sb = new StringBuilder();
            sb.Append($"<BitmapIcon{GetTemplateBindings(element, dataContext)}>");
            if (element.ContextFlyout?.Content != null)
            {
                sb.Append("<BitmapIcon.ContextFlyout>");
                sb.Append($"<Flyout>");
                sb.Append(GetTemplate((dynamic)element.ContextFlyout.Content, null));
                sb.Append("</Flyout>");
                sb.Append("</BitmapIcon.ContextFlyout>");
            }
            sb.Append($"</BitmapIcon>");
            return sb.ToString();
        }

        public string GetTemplateBindings(BitmapIcon element, string dataContext)
        {
            var sb = new StringBuilder();
            sb.Append(GetTemplateBindings((Icon)element, dataContext));
            sb.Append(" binders:BitmapIconBinder.Element=\"{Binding Mode=OneTime}\"");
            return sb.ToString();
        }
    }
}
