using System;
using System.Windows;
using System.Threading.Tasks;
using System.Text;

using Globeport.Shared.Library.Xaml;

namespace Globeport.Shared.Library.Xaml.UWP
{
    public partial class Renderer
    {
        public string GetTemplate(TextBlock element, string dataContext)
        {
            var sb = new StringBuilder();
            sb.Append($"<TextBlock{GetTemplateBindings(element, dataContext)}>");
            if (element.ContextFlyout?.Content != null)
            {
                sb.Append("<TextBlock.ContextFlyout>");
                sb.Append($"<Flyout>");
                sb.Append(GetTemplate((dynamic)element.ContextFlyout.Content, null));
                sb.Append("</Flyout>"); ;
                sb.Append("</TextBlock.ContextFlyout>");
            }
            sb.Append($"</TextBlock>");
            return sb.ToString();
        }

        public string GetTemplateBindings(TextBlock element, string dataContext)
        {
            var sb = new StringBuilder();
            sb.Append(GetTemplateBindings((FrameworkElement)element, dataContext));
            sb.Append(" binders:TextBlockBinder.Element=\"{Binding Mode=OneTime}\"");
            return sb.ToString();
        }
    }
}
