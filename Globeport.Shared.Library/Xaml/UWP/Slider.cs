using System.Windows;
using System.Threading.Tasks;
using System.Text;

using Globeport.Shared.Library.Xaml;

namespace Globeport.Shared.Library.Xaml.UWP
{
    public partial class Renderer
    {
        public string GetTemplate(Slider element, string dataContext)
        {
            var sb = new StringBuilder();
            sb.Append($"<Slider{GetTemplateBindings(element, dataContext)}>");
            if (element.Background is ImageBrush)
            {
                sb.Append("<Slider.Background>");
                sb.Append(GetTemplate((ImageBrush)element.Background, "Background"));
                sb.Append("</Slider.Background>");
            }
            if (element.ContextFlyout?.Content != null)
            {
                sb.Append("<Slider.ContextFlyout>");
                sb.Append($"<Flyout>");
                sb.Append(GetTemplate((dynamic)element.ContextFlyout.Content, null));
                sb.Append("</Flyout>");
                sb.Append("</Slider.ContextFlyout>");
            }
            sb.Append($"</Slider>");
            return sb.ToString();
        }

        public string GetTemplateBindings(Slider element, string dataContext)
        {
            var sb = new StringBuilder();
            sb.Append(GetTemplateBindings((Control)element, dataContext));
            sb.Append(" binders:SliderBinder.Element=\"{Binding Mode=OneTime}\"");
            return sb.ToString();
        }
    }
}
