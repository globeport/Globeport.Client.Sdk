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
        public string GetTemplate(FlipView element, string dataContext)
        {
            var sb = new StringBuilder();
            sb.Append($"<FlipView{GetTemplateBindings(element, dataContext)}>");
            if (element.ItemTemplate?.Template != null)
            {
                sb.Append("<FlipView.ItemTemplate>");
                sb.Append("<DataTemplate>");
                sb.Append(GetTemplate((dynamic)element.ItemTemplate.Template, null));
                sb.Append("</DataTemplate>");
                sb.Append("</FlipView.ItemTemplate>");
            }
            if (element.ItemsPanel?.Template != null)
            {
                sb.Append("<FlipView.ItemsPanel>");
                sb.Append("<ItemsPanelTemplate>");
                sb.Append(GetTemplate((dynamic)element.ItemsPanel.Template, "ItemsPanel.Template"));
                sb.Append("</ItemsPanelTemplate>");
                sb.Append("</FlipView.ItemsPanel>");
            }
            else
            {
                sb.Append("<FlipView.ItemsPanel>");
                sb.Append("<ItemsPanelTemplate>");
                sb.Append("<StackPanel Orientation=\"Horizontal\"/>");
                sb.Append("</ItemsPanelTemplate>");
                sb.Append("</FlipView.ItemsPanel>");
            }
            if (element.ContextFlyout?.Content != null)
            {
                sb.Append("<FlipView.ContextFlyout>");
                sb.Append($"<Flyout>");
                sb.Append(GetTemplate((dynamic)element.ContextFlyout.Content, null));
                sb.Append("</Flyout>");
                sb.Append("</FlipView.ContextFlyout>");
            }
            sb.Append("</FlipView>");
            return sb.ToString();
        }

        public string GetTemplateBindings(FlipView element, string dataContext)
        {
            var sb = new StringBuilder();
            sb.Append(GetTemplateBindings((Selector)element, dataContext));
            sb.Append(" binders:FlipViewBinder.Element=\"{Binding Mode=OneTime}\"");
            return sb.ToString();
        }
    }
}
