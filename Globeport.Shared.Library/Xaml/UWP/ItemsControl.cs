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
        public string GetTemplate(ItemsControl element, string dataContext)
        {
            var sb = new StringBuilder();
            sb.Append($"<ItemsControl{GetTemplateBindings(element, dataContext)}>");
            if (element.Background is ImageBrush)
            {
                sb.Append("<ItemsControl.Background>");
                sb.Append(GetTemplate((ImageBrush)element.Background, "Background"));
                sb.Append("</ItemsControl.Background>");
            }
            if (element.ItemTemplate?.Template != null)
            {
                sb.Append("<ItemsControl.ItemTemplate>");
                sb.Append("<DataTemplate>");
                sb.Append(GetTemplate((dynamic)element.ItemTemplate.Template, null));
                sb.Append("</DataTemplate>");
                sb.Append("</ItemsControl.ItemTemplate>");
            }
            if (element.ItemsPanel?.Template!=null)
            {
                sb.Append("<ItemsControl.ItemsPanel>");
                sb.Append("<ItemsPanelTemplate>");
                sb.Append(GetTemplate((dynamic)element.ItemsPanel.Template, "ItemsPanel.Template"));
                sb.Append("</ItemsPanelTemplate>");
                sb.Append("</ItemsControl.ItemsPanel>");
            }
            if (element.ContextFlyout?.Content != null)
            {
                sb.Append("<ItemsControl.ContextFlyout>");
                sb.Append($"<Flyout>");
                sb.Append(GetTemplate((dynamic)element.ContextFlyout.Content, null));
                sb.Append("</Flyout>");
                sb.Append("</ItemsControl.ContextFlyout>");
            }
            sb.Append("</ItemsControl>");
            return sb.ToString();
        }

        public string GetTemplateBindings(ItemsControl element, string dataContext)
        {
            var sb = new StringBuilder();
            sb.Append(GetTemplateBindings((Control)element, dataContext));
            sb.Append(" binders:ItemsControlBinder.Element=\"{Binding Mode=OneTime}\"");
            return sb.ToString();
        }
    }
}
