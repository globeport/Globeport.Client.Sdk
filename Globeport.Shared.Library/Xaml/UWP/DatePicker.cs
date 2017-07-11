using System;
using System.Windows;
using System.Threading.Tasks;
using System.Text;

namespace Globeport.Shared.Library.Xaml.UWP
{
    public partial class Renderer
    {
        public string GetTemplate(DatePicker element, string dataContext)
        {
            var sb = new StringBuilder();
            sb.Append($"<DatePicker{GetTemplateBindings(element, dataContext)}>");
            if (element.Background is ImageBrush)
            {
                sb.Append("<DatePicker.Background>");
                sb.Append(GetTemplate((ImageBrush)element.Background, "Background"));
                sb.Append("</DatePicker.Background>");
            }
            if (element.ContextFlyout?.Content != null)
            {
                sb.Append("<DatePicker.ContextFlyout>");
                sb.Append($"<Flyout>");
                sb.Append(GetTemplate((dynamic)element.ContextFlyout.Content, null));
                sb.Append("</Flyout>");
                sb.Append("</DatePicker.ContextFlyout>");
            }
            sb.Append($"</DatePicker>");
            return sb.ToString();
        }

        public string GetTemplateBindings(DatePicker element, string dataContext)
        {
            var sb = new StringBuilder();
            sb.Append(GetTemplateBindings((Control)element, dataContext));
            sb.Append(" binders:DatePickerBinder.Element=\"{Binding Mode=OneTime}\"");
            return sb.ToString();
        }
    }
}
