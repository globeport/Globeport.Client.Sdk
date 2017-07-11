using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Globeport.Shared.Library.Xaml;

namespace Globeport.Shared.Library.Xaml.UWP
{
    public partial class Renderer
    {
        public string GetTemplate(Rectangle element, string dataContext)
        {
            var sb = new StringBuilder();
            sb.Append($"<Rectangle{GetTemplateBindings(element, dataContext)}>");
            if (element.Fill is ImageBrush)
            {
                sb.Append("<Rectangle.Fill>");
                sb.Append(GetTemplate((ImageBrush)element.Fill, "Fill"));
                sb.Append("</Rectangle.Fill>");
            }
            if (element.ContextFlyout?.Content != null)
            {
                sb.Append("<Rectangle.ContextFlyout>");
                sb.Append($"<Flyout>");
                sb.Append(GetTemplate((dynamic)element.ContextFlyout.Content, null));
                sb.Append("</Flyout>");
                sb.Append("</Rectangle.ContextFlyout>");
            }
            sb.Append("</Rectangle>");
            return sb.ToString();
        }

        public string GetTemplateBindings(Rectangle element, string dataContext)
        {
            var sb = new StringBuilder();
            sb.Append(GetTemplateBindings((Shape)element, dataContext));
            sb.Append(" binders:RectangleBinder.Element=\"{Binding Mode=OneTime}\"");
            return sb.ToString();
        }
    }
}
