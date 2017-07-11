using System;
using System.Windows;
using System.Threading.Tasks;
using System.Text;
using System.Linq;

using Globeport.Shared.Library.Xaml;

namespace Globeport.Shared.Library.Xaml.UWP
{
    public partial class Renderer
    {
        public string GetTemplate(ImageBrush element, string dataContext)
        {
            var sb = new StringBuilder();
            sb.Append($"<ImageBrush {GetTemplateBindings(element, dataContext)}/>");
            return sb.ToString();
        }

        public string GetTemplateBindings(ImageBrush element, string dataContext)
        {
            var sb = new StringBuilder();
            sb.Append($" binders:ImageBrushBinder.Element=\"{{Binding {dataContext},Mode=OneTime}}\"");
            return sb.ToString();
        }
    }
}
