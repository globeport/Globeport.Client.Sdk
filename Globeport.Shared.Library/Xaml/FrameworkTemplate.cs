using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Portable.Xaml.Markup;

using Globeport.Shared.Library.Interfaces;

namespace Globeport.Shared.Library.Xaml
{
    [ContentProperty("Template")]
    public class FrameworkTemplate : DependencyObject, IFrameworkTemplate
    {
        public override string Type => nameof(FrameworkTemplate);
        public FrameworkElement Template { get; set; }

        public override object Clone()
        {
            var element = new FrameworkTemplate();
            element.CopyFrom(this);
            return element;
        }

        public override void CopyFrom(DependencyObject element)
        {
            var source = (FrameworkTemplate)element;
            Template = (FrameworkElement)source.Template?.Clone();
            base.CopyFrom(source);
        }
    }
}
