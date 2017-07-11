using System;
using System.Windows;
using System.Threading.Tasks;
using System.Text;
using System.Linq;

using Globeport.Shared.Library.Xaml;

namespace Globeport.Shared.Library.Xaml.UWP
{
    public partial class Renderer
    {
        public string GetTemplate(ItemsStackPanel element, string dataContext)
        {
            var sb = new StringBuilder();
            sb.Append($"<ItemsStackPanel {GetTemplateBindings(element, dataContext)}>");
            if (element.Background is ImageBrush)
            {
                sb.Append("<ItemsStackPanel.Background>");
                sb.Append(GetTemplate((ImageBrush)element.Background, "Background"));
                sb.Append("</ItemsStackPanel.Background>");
            }
            if (element.ContextFlyout?.Content != null)
            {
                sb.Append("<ItemsStackPanel.ContextFlyout>");
                sb.Append($"<Flyout>");
                sb.Append(GetTemplate((dynamic)element.ContextFlyout.Content, null));
                sb.Append("</Flyout>");
                sb.Append("</ItemsStackPanel.ContextFlyout>");
            }
            sb.Append($"</ItemsStackPanel>");
            return sb.ToString();
        }

        public string GetTemplateBindings(ItemsStackPanel element, string dataContext)
        {
            var sb = new StringBuilder();
            sb.Append(GetTemplateBindings((Panel)element, dataContext));
            if (element.Orientation!=null)
            {
                sb.Append($" Orientation=\"{element.Orientation}\"");
            }
            return sb.ToString();
        }
    }
}
