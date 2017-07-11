using System.Windows;
using System.Threading.Tasks;
using System.Text;

using Globeport.Shared.Library.Xaml;

namespace Globeport.Shared.Library.Xaml.UWP
{
    public partial class Renderer
    {
        public string GetTemplate(Grid element, string dataContext)
        {
            var sb = new StringBuilder();
            sb.Append($"<Grid{GetTemplateBindings(element, dataContext)}>");
            if (element.Background is ImageBrush)
            {
                sb.Append("<Grid.Background>");
                sb.Append(GetTemplate((ImageBrush)element.Background, "Background"));
                sb.Append("</Grid.Background>");
            }
            if (element.ColumnDefinitions != null && element.ColumnDefinitions.Count > 0)
            {
                sb.Append("<Grid.ColumnDefinitions>");
                var index = 0;
                foreach (var col in element.ColumnDefinitions)
                {
                    sb.Append($"<ColumnDefinition{GetTemplateBindings(col, $"ColumnDefinitions[{index}]")}/>");
                    index++;
                }
                sb.Append("</Grid.ColumnDefinitions>");
            }
            if (element.RowDefinitions != null && element.RowDefinitions.Count > 0)
            {
                sb.Append("<Grid.RowDefinitions>");
                var index = 0;
                foreach (var row in element.RowDefinitions)
                {
                    sb.Append($"<RowDefinition{GetTemplateBindings(row, $"RowDefinitions[{index}]")}/>");
                    index++;
                }
                sb.Append("</Grid.RowDefinitions>");
            }
            if (element.Children != null)
            {
                var index = 0;
                foreach (var child in element.Children)
                {
                    sb.Append(GetTemplate((dynamic)child, $"Children[{index}]"));
                    index++;
                }
            }
            if (element.ContextFlyout?.Content != null)
            {
                sb.Append("<Grid.ContextFlyout>");
                sb.Append($"<Flyout>");
                sb.Append(GetTemplate((dynamic)element.ContextFlyout.Content, null));
                sb.Append("</Flyout>");
                sb.Append("</Grid.ContextFlyout>");
            }
            sb.Append("</Grid>");
            return sb.ToString();
        }

        public string GetTemplateBindings(Grid element, string dataContext)
        {
            var sb = new StringBuilder();
            sb.Append(GetTemplateBindings((Panel)element, dataContext));
            sb.Append(" binders:GridBinder.Element=\"{Binding Mode=OneTime}\"");
            return sb.ToString();
        }

        public string GetTemplateBindings(ColumnDefinition element, string dataContext)
        {
            var sb = new StringBuilder();
            sb.Append($" binders:ColumnDefinitionBinder.Element=\"{{Binding {dataContext},Mode=OneTime}}\"");
            return sb.ToString();
        }

        public string GetTemplateBindings(RowDefinition element, string dataContext)
        {
            var sb = new StringBuilder();
            sb.Append($" binders:RowDefinitionBinder.Element=\"{{Binding {dataContext},Mode=OneTime}}\"");
            return sb.ToString();
        }
    }
}
