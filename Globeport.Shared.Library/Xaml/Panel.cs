using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Collections.ObjectModel;

using Portable.Xaml.Markup;

using Globeport.Shared.Library.Interfaces;

namespace Globeport.Shared.Library.Xaml
{
    [ContentProperty("Children")]
    public class Panel : FrameworkElement, IPanel
    {
        public override string Type => nameof(Panel);
        public FrameworkElements Children { get; set; } = new FrameworkElements();

        public Panel()
        {
        }

        public override List<DependencyObject> GetElements()
        {
            var elements = base.GetElements();
            if (Background is ImageBrush)
            {
                elements.AddRange(((ImageBrush)Background).GetElements());
            }
            foreach (var element in Children)
            {
                elements.AddRange(element.GetElements());
            }
            return elements;
        }

        public override object Clone()
        {
            var element = new Panel();
            element.CopyFrom(this);
            return element;
        }

        public override void CopyFrom(DependencyObject element)
        {
            var source = (Panel)element;
            if (source.Background is ImageBrush)
            {
                Background = ((ImageBrush)source.Background).Clone();
            }
            else
            {
                Background = source.Background;
            }
            Children = new FrameworkElements(source.Children.Select(i => (FrameworkElement)i.Clone()));
            base.CopyFrom(source);
        }

        object background;
        public object Background
        {
            get
            {
                return background;
            }
            set
            {
                if (value != background & (value == null || value is ImageBrush || value is string))
                {
                    background = value;
                    OnPropertyChanged(nameof(Background));
                }
            }
        }
    }
}
