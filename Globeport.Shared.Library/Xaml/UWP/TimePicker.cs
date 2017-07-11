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
        public string GetTemplate(TimePicker element, string dataContext)
        {
            var sb = new StringBuilder();
            sb.Append($"<TimePicker{GetTemplateBindings(element, dataContext)}>");
            if (element.Background is ImageBrush)
            {
                sb.Append("<TimePicker.Background>");
                sb.Append(GetTemplate((ImageBrush)element.Background, "Background"));
                sb.Append("</TimePicker.Background>");
            }
            if (element.ContextFlyout?.Content != null)
            {
                sb.Append("<TimePicker.ContextFlyout>");
                sb.Append($"<Flyout>");
                sb.Append(GetTemplate((dynamic)element.ContextFlyout.Content, null));
                sb.Append("</Flyout>");
                sb.Append("</TimePicker.ContextFlyout>");
            }
            sb.Append($"</TimePicker>");
            return sb.ToString();
        }

        public string GetTemplateBindings(TimePicker element, string dataContext)
        {
            var sb = new StringBuilder();
            sb.Append(GetTemplateBindings((Control)element, dataContext));
            sb.Append(" binders:TimePickerBinder.Element=\"{Binding Mode=OneTime}\"");
            return sb.ToString();
        }
    }
}
