using System;
using System.Windows;
using System.Threading.Tasks;
using System.Text;

using Globeport.Shared.Library.Xaml;

namespace Globeport.Shared.Library.Xaml.UWP
{
    public partial class Renderer
    {
        public string GetTemplate(PasswordBox element, string dataContext)
        {
            var sb = new StringBuilder();
            sb.Append($"<PasswordBox{GetTemplateBindings(element, dataContext)}>");
            if (element.Background is ImageBrush)
            {
                sb.Append("<PasswordBox.Background>");
                sb.Append(GetTemplate((ImageBrush)element.Background, "Background"));
                sb.Append("</PasswordBox.Background>");
            }
            if (element.ContextFlyout?.Content != null)
            {
                sb.Append("<PasswordBox.ContextFlyout>");
                sb.Append($"<Flyout>");
                sb.Append(GetTemplate((dynamic)element.ContextFlyout.Content, null));
                sb.Append("</Flyout>");
                sb.Append("</PasswordBox.ContextFlyout>");
            }
            sb.Append($"</PasswordBox>");
            return sb.ToString();
        }

        public string GetTemplateBindings(PasswordBox element, string dataContext)
        {
            var sb = new StringBuilder();
            sb.Append(GetTemplateBindings((Control)element, dataContext));
            sb.Append(" binders:PasswordBoxBinder.Element=\"{Binding Mode=OneTime}\"");
            return sb.ToString();
        }
    }
}
