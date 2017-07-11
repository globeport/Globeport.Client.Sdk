using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Portable.Xaml.Markup;

using Globeport.Shared.Library.Interfaces;

namespace Globeport.Shared.Library.Xaml
{
    [ContentProperty("Child")]
    public class Border : FrameworkElement, IBorder
    {
        public override string Type => nameof(Border);

        public Border()
        {
        }

        public override List<DependencyObject> GetElements()
        {
            var elements = base.GetElements();
            if (Child != null) elements.AddRange(Child.GetElements());
            return elements;
        }

        public override object Clone()
        {
            var element = new Border();
            element.CopyFrom(this);
            return element;
        }

        public override void CopyFrom(DependencyObject element)
        {
            var source = (Border)element;
            CornerRadius = source.CornerRadius;
            Background = source.Background;
            BorderThickness = source.BorderThickness;
            BorderBrush = source.BorderBrush;
            Padding = source.Padding;
            Child = (FrameworkElement)source.Child?.Clone();
            base.CopyFrom(source);
        }

        FrameworkElement child;
        public FrameworkElement Child
        {
            get
            {
                return child;
            }
            set
            {
                if (value != child)
                {
                    child = value;
                    OnPropertyChanged(nameof(Child));
                }
            }
        }

        string cornerRadius;
        public string CornerRadius
        {
            get
            {
                return cornerRadius;
            }
            set
            {
                if (value != cornerRadius)
                {
                    cornerRadius = value;
                    OnPropertyChanged(nameof(CornerRadius));
                }
            }
        }

        string background;
        public string Background
        {
            get
            {
                return background;
            }
            set
            {
                if (background != value)
                {
                    background = value;
                    OnPropertyChanged(nameof(Background));
                }
            }
        }

        string borderThickness;
        public string BorderThickness
        {
            get
            {
                return borderThickness;
            }
            set
            {
                if (value != borderThickness)
                {
                    borderThickness = value;
                    OnPropertyChanged(nameof(BorderThickness));
                }
            }
        }

        string borderBrush;
        public string BorderBrush
        {
            get
            {
                return borderBrush;
            }
            set
            {
                if (value != borderBrush)
                {
                    borderBrush = value;
                    OnPropertyChanged(nameof(BorderBrush));
                }
            }
        }

        string padding;
        public string Padding
        {
            get
            {
                return padding;
            }
            set
            {
                if (padding != value)
                {
                    padding = value;
                    OnPropertyChanged(nameof(Padding));
                }
            }
        }
    }
}
