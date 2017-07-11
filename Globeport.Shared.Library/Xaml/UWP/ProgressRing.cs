using System.Windows;
using System.Threading.Tasks;
using System.Text;

using Globeport.Shared.Library.Xaml;

namespace Globeport.Shared.Library.Xaml.UWP
{
    public partial class Renderer
    {
        public string GetTemplate(ProgressRing element, string dataContext)
        {
            var sb = new StringBuilder();
            sb.Append($"<ProgressRing{GetTemplateBindings(element, dataContext)}>");
            if (element.Background is ImageBrush)
            {
                sb.Append("<ProgressRing.Background>");
                sb.Append(GetTemplate((ImageBrush)element.Background, "Background"));
                sb.Append("</ProgressRing.Background>");
            }
            if (element.ContextFlyout?.Content != null)
            {
                sb.Append("<ProgressRing.ContextFlyout>");
                sb.Append($"<Flyout>");
                sb.Append(GetTemplate((dynamic)element.ContextFlyout.Content, null));
                sb.Append("</Flyout>");
                sb.Append("</ProgressRing.ContextFlyout>");
            }
            sb.Append($"</ProgressRing>");
            return sb.ToString();
        }

        public string GetTemplateBindings(ProgressRing element, string dataContext)
        {
            var sb = new StringBuilder();
            sb.Append(GetTemplateBindings((Control)element, dataContext));
            sb.Append(" binders:ProgressRingBinder.Element=\"{Binding Mode=OneTime}\"");
            return sb.ToString();
        }
    }
}
