using System.Windows;
using System.Threading.Tasks;
using System.Text;

using Globeport.Shared.Library.Xaml;

namespace Globeport.Shared.Library.Xaml.UWP
{
    public partial class Renderer
    {
        public string GetTemplate(ProgressBar element, string dataContext)
        {
            var sb = new StringBuilder();
            sb.Append($"<ProgressBar{GetTemplateBindings(element, dataContext)}>");
            if (element.Background is ImageBrush)
            {
                sb.Append("<ProgressBar.Background>");
                sb.Append(GetTemplate((ImageBrush)element.Background, "Background"));
                sb.Append("</ProgressBar.Background>");
            }
            if (element.ContextFlyout?.Content != null)
            {
                sb.Append("<ProgressBar.ContextFlyout>");
                sb.Append($"<Flyout>");
                sb.Append(GetTemplate((dynamic)element.ContextFlyout.Content, null));
                sb.Append("</Flyout>");
                sb.Append("</ProgressBar.ContextFlyout>");
            }
            sb.Append($"</ProgressBar>");
            return sb.ToString();
        }

        public string GetTemplateBindings(ProgressBar element, string dataContext)
        {
            var sb = new StringBuilder();
            sb.Append(GetTemplateBindings((Control)element, dataContext));
            sb.Append(" binders:ProgressBarBinder.Element=\"{Binding Mode=OneTime}\"");
            return sb.ToString();
        }
    }
}
