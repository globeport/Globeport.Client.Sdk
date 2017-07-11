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
        public string GetTemplate(Ellipse element, string dataContext)
        {
            var sb = new StringBuilder();
            sb.Append($"<Ellipse{GetTemplateBindings(element, dataContext)}>");
            if (element.Fill is ImageBrush)
            {
                sb.Append("<Ellipse.Fill>");
                sb.Append(GetTemplate((ImageBrush)element.Fill, "Fill"));
                sb.Append("</Ellipse.Fill>");
            }
            if (element.ContextFlyout?.Content != null)
            {
                sb.Append("<Ellipse.ContextFlyout>");
                sb.Append($"<Flyout>");
                sb.Append(GetTemplate((dynamic)element.ContextFlyout.Content, null));
                sb.Append("</Flyout>");
                sb.Append("</Ellipse.ContextFlyout>");
            }
            sb.Append("</Ellipse>");
            return sb.ToString();
        }
    }
}
