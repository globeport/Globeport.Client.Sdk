using System;
using System.Linq;
using System.Windows;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text;

using Globeport.Shared.Library.Xaml;

namespace Globeport.Shared.Library.Xaml.UWP
{
    public partial class Renderer
    {
        public string GetTemplate(GridView element, string dataContext)
        {
            var sb = new StringBuilder();
            sb.Append($"<GridView{GetTemplateBindings(element, dataContext)}>");
            if (element.ItemTemplate?.Template != null)
            {
                sb.Append("<GridView.ItemTemplate>");
                sb.Append("<DataTemplate>");
                sb.Append(GetTemplate((dynamic)element.ItemTemplate.Template, null));
                sb.Append("</DataTemplate>");
                sb.Append("</GridView.ItemTemplate>");
            }
            if (element.ItemsPanel?.Template != null)
            {
                sb.Append("<GridView.ItemsPanel>");
                sb.Append("<ItemsPanelTemplate>");
                sb.Append(GetTemplate((dynamic)element.ItemsPanel.Template, "ItemsPanel.Template"));
                sb.Append("</ItemsPanelTemplate>");
                sb.Append("</GridView.ItemsPanel>");
            }
            if (element.Header is FrameworkElement)
            {
                sb.Append("<GridView.Header>");
                sb.Append(GetTemplate((dynamic)element.Header, "Header"));
                sb.Append("<GridView.Header>");
            }
            if (element.ContextFlyout?.Content != null)
            {
                sb.Append("<GridView.ContextFlyout>");
                sb.Append($"<Flyout>");
                sb.Append(GetTemplate((dynamic)element.ContextFlyout.Content, null));
                sb.Append("</Flyout>");
                sb.Append("</GridView.ContextFlyout>");
            }
            sb.Append("</GridView>");
            return sb.ToString();
        }
    }
}
