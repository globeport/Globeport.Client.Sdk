using System.Windows;
using System.Threading.Tasks;
using System.Text;

using Globeport.Shared.Library.Xaml;

namespace Globeport.Shared.Library.Xaml.UWP
{
    public partial class Renderer
    { 
        public string GetTemplate(StackPanel element, string dataContext)
        {
            var sb = new StringBuilder();
            sb.Append($"<StackPanel{GetTemplateBindings(element, dataContext)}>");
            if (element.Background is ImageBrush)
            {
                sb.Append("<StackPanel.Background>");
                sb.Append(GetTemplate((ImageBrush)element.Background, "Background"));
                sb.Append("</StackPanel.Background>");
            }
            if (element.Children != null)
            {
                var index = 0;
                foreach (var child in element.Children)
                {
                    sb.Append(GetTemplate((dynamic)child, "Children[" + index + "]"));
                    index++;
                }
            }
            if (element.ContextFlyout?.Content != null)
            {
                sb.Append("<StackPanel.ContextFlyout>");
                sb.Append($"<Flyout>");
                sb.Append(GetTemplate((dynamic)element.ContextFlyout.Content, null));
                sb.Append("</Flyout>");
                sb.Append("</StackPanel.ContextFlyout>");
            }
            sb.Append("</StackPanel>");
            return sb.ToString();
        }

        public string GetTemplateBindings(StackPanel element, string dataContext)
        {
            var sb = new StringBuilder();
            sb.Append(GetTemplateBindings((Panel)element, dataContext));
            sb.Append(" binders:StackPanelBinder.Element=\"{Binding Mode=OneTime}\"");
            return sb.ToString();
        }
    }
}
