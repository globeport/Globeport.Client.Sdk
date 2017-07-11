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
        public string GetTemplate(ItemsWrapGrid element, string dataContext)
        {
            var sb = new StringBuilder();
            sb.Append($"<ItemsWrapGrid {GetTemplateBindings(element, dataContext)}>");
            if (element.Background is ImageBrush)
            {
                sb.Append("<ItemsWrapGrid.Background>");
                sb.Append(GetTemplate((ImageBrush)element.Background, "Background"));
                sb.Append("</ItemsWrapGrid.Background>");
            }
            if (element.ContextFlyout?.Content != null)
            {
                sb.Append("<ItemsWrapGrid.ContextFlyout>");
                sb.Append($"<Flyout>");
                sb.Append(GetTemplate((dynamic)element.ContextFlyout.Content, null));
                sb.Append("</Flyout>");
                sb.Append("</ItemsWrapGrid.ContextFlyout>");
            }
            sb.Append($"</ItemsWrapGrid>");
            return sb.ToString();
        }

        public string GetTemplateBindings(ItemsWrapGrid element, string dataContext)
        {
            var sb = new StringBuilder();
            sb.Append(GetTemplateBindings((Panel)element, dataContext));
            if (element.Orientation!=null)
            {
                sb.Append($" Orientation=\"{element.Orientation}\"");
            }
            if (element.MaximumRowsOrColumns >= 0)
            {
                sb.Append($" MaximumRowsOrColumns=\"{element.MaximumRowsOrColumns}\"");
            }
            return sb.ToString();
        }
    }
}
