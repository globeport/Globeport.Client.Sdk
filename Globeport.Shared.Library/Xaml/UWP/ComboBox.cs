using System;
using System.Linq;
using System.Windows;
using System.Threading.Tasks;
using System.Text;

using Globeport.Shared.Library.Xaml;

namespace Globeport.Shared.Library.Xaml.UWP
{
    public partial class Renderer
    {
        public string GetTemplate(ComboBox element, string dataContext)
        {
            var sb = new StringBuilder();
            sb.Append($"<ComboBox{GetTemplateBindings(element, dataContext)}>");
            if (element.ItemTemplate?.Template != null)
            {
                sb.Append("<ComboBox.ItemTemplate>");
                sb.Append("<DataTemplate>");
                sb.Append(GetTemplate((dynamic)element.ItemTemplate.Template, null));
                sb.Append("</DataTemplate>");
                sb.Append("</ComboBox.ItemTemplate>");
            }
            if (element.ItemsPanel?.Template != null)
            {
                sb.Append("<ComboBox.ItemsPanel>");
                sb.Append("<ItemsPanelTemplate>");
                sb.Append(GetTemplate((dynamic)element.ItemsPanel.Template, "ItemsPanel.Template"));
                sb.Append("</ItemsPanelTemplate>");
                sb.Append("</ComboBox.ItemsPanel>");
            }
            if (element.ContextFlyout?.Content != null)
            {
                sb.Append("<ComboBox.ContextFlyout>");
                sb.Append($"<Flyout>");
                sb.Append(GetTemplate((dynamic)element.ContextFlyout.Content, null));
                sb.Append("</Flyout>");
                sb.Append("</ComboBox.ContextFlyout>");
            }
            sb.Append("</ComboBox>");
            return sb.ToString();
        }

        public string GetTemplateBindings(ComboBox element, string dataContext)
        {
            var sb = new StringBuilder();
            sb.Append(GetTemplateBindings((Selector)element, dataContext));
            sb.Append(" binders:ComboBoxBinder.Element=\"{Binding Mode=OneTime}\"");
            return sb.ToString();
        }
    }
}
