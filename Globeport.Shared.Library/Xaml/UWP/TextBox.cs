using System;
using System.Windows;
using System.Threading.Tasks;
using System.Text;

using Globeport.Shared.Library.Xaml;
using Globeport.Shared.Library.Extensions;

namespace Globeport.Shared.Library.Xaml.UWP
{
    public partial class Renderer
    {
        public string GetTemplate(TextBox element, string dataContext)
        {
            var sb = new StringBuilder();
            sb.Append($"<TextBox{GetTemplateBindings(element, dataContext)}>");
            if (element.Background is ImageBrush)
            {
                sb.Append("<TextBox.Background>");
                sb.Append(GetTemplate((ImageBrush)element.Background, "Background"));
                sb.Append("</TextBox.Background>");
            }
            if (element.ContextFlyout?.Content != null)
            {
                sb.Append("<TextBox.ContextFlyout>");
                sb.Append($"<Flyout>");
                sb.Append(GetTemplate((dynamic)element.ContextFlyout.Content, null));
                sb.Append("</Flyout>");
                sb.Append("</TextBox.ContextFlyout>");
            }
            sb.Append($"</TextBox>");
            return sb.ToString();
        }

        public string GetTemplateBindings(TextBox element, string dataContext)
        {
            var sb = new StringBuilder();
            sb.Append(GetTemplateBindings((Control)element, dataContext));
            sb.Append(" binders:TextBoxBinder.Element=\"{Binding Mode=OneTime}\"");
            return sb.ToString();
        }
    }
}
