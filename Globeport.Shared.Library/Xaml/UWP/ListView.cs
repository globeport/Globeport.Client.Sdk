using System;
using System.Linq;
using System.Windows;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text;

using Globeport.Shared.Library.Xaml;
using Globeport.Shared.Library.Extensions;

namespace Globeport.Shared.Library.Xaml.UWP
{
    public partial class Renderer
    {
        public string GetTemplate(ListView element, string dataContext)
        {
            var sb = new StringBuilder();
            sb.Append($"<ListView{GetTemplateBindings(element, dataContext)}>");
            if (element.ItemTemplate?.Template != null)
            {
                sb.Append("<ListView.ItemTemplate>");
                sb.Append("<DataTemplate>");
                sb.Append(GetTemplate((dynamic)element.ItemTemplate.Template, null));
                sb.Append("</DataTemplate>");
                sb.Append("</ListView.ItemTemplate>");
            }
            if (element.ItemsPanel?.Template != null)
            {
                sb.Append("<ListView.ItemsPanel>");
                sb.Append("<ItemsPanelTemplate>");
                sb.Append(GetTemplate((dynamic)element.ItemsPanel.Template, "ItemsPanel.Template"));
                sb.Append("</ItemsPanelTemplate>");
                sb.Append("</ListView.ItemsPanel>");
            }
            if (element.Header is FrameworkElement)
            {
                sb.Append("<ListView.Header>");
                sb.Append(GetTemplate((dynamic)element.Header, "Header"));
                sb.Append("</ListView.Header>");
            }
            if (element.ContextFlyout?.Content != null)
            {
                sb.Append("<ListView.ContextFlyout>");
                sb.Append($"<Flyout>");
                sb.Append(GetTemplate((dynamic)element.ContextFlyout.Content, null));
                sb.Append("</Flyout>");
                sb.Append("</ListView.ContextFlyout>");
            }
            sb.Append("</ListView>");
            return sb.ToString();
        }

        public string GetTemplateBindings(ListView element, string dataContext)
        {
            var sb = new StringBuilder();
            sb.Append(GetTemplateBindings((Selector)element, dataContext));
            sb.Append(" binders:ListViewBinder.Element=\"{Binding Mode=OneTime}\"");
            sb.Append(" binders:ListViewBinder.SelectedItems=\"{Binding SelectedItems}\"");
            return sb.ToString();
        }
    }
}
