using System;
using System.Windows;
using System.Threading.Tasks;
using System.Text;

namespace Globeport.Shared.Library.Xaml.UWP
{
    public partial class Renderer
    {
        public string GetTemplate(CalendarDatePicker element, string dataContext)
        {
            var sb = new StringBuilder();
            sb.Append($"<CalendarDatePicker{GetTemplateBindings(element, dataContext)}>");
            if (element.Background is ImageBrush)
            {
                sb.Append("<CalendarDatePicker.Background>");
                sb.Append(GetTemplate((ImageBrush)element.Background, "Background"));
                sb.Append("</CalendarDatePicker.Background>");
            }
            if (element.ContextFlyout?.Content != null)
            {
                sb.Append("<CalendarDatePicker.ContextFlyout>");
                sb.Append($"<Flyout>");
                sb.Append(GetTemplate((dynamic)element.ContextFlyout.Content, null));
                sb.Append("</Flyout>");
                sb.Append("</CalendarDatePicker.ContextFlyout>");
            }
            sb.Append($"</CalendarDatePicker>");
            return sb.ToString();
        }

        public string GetTemplateBindings(CalendarDatePicker element, string dataContext)
        {
            var sb = new StringBuilder();
            sb.Append(GetTemplateBindings((Control)element, dataContext));
            sb.Append(" binders:CalendarDatePickerBinder.Element=\"{Binding Mode=OneTime}\"");
            return sb.ToString();
        }
    }
}
