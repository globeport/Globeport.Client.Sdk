using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Globeport.Shared.Library.Interfaces;
using Globeport.Shared.Library.Extensions;

namespace Globeport.Shared.Library.Xaml
{
    public class Shape : FrameworkElement, IShape
    {
        public override string Type => nameof(Shape);

        public Shape()
        {
        }

        public override List<DependencyObject> GetElements()
        {
            var elements = base.GetElements();
            if (Fill is ImageBrush)
            {
                elements.AddRange(((ImageBrush)Fill).GetElements());
            }
            return elements;
        }

        public override object Clone()
        {
            var element = new Shape();
            element.CopyFrom(this);
            return element;
        }

        public override void CopyFrom(DependencyObject element)
        {
            var source = (Shape)element;
            if (source.Fill is ImageBrush) 
            {
                Fill = ((ImageBrush)source.Fill).Clone();
            }
            else
            {
                Fill = source.Fill;
            }
            StrokeThickness = source.StrokeThickness;
            Stroke = source.Stroke;
            Stretch = source.Stretch;
            base.CopyFrom(source);
        }

        object fill;
        public object Fill
        {
            get
            {
                return fill;
            }
            set
            {
                if (value != fill & (value == null || value is ImageBrush || value is string))
                {
                    fill = value;
                    OnPropertyChanged(nameof(Fill));
                }
            }
        }


        double strokeThickness;
        public double StrokeThickness
        {
            get
            {
                return strokeThickness;
            }
            set
            {
                if (value != strokeThickness)
                {
                    strokeThickness = value;
                    OnPropertyChanged(nameof(StrokeThickness));
                }
            }
        }

        string stroke;
        public string Stroke
        {
            get
            {
                return stroke;
            }
            set
            {
                if (value != stroke)
                {
                    stroke = value;
                    OnPropertyChanged(nameof(Stroke));
                }
            }
        }

        string stretch;
        public string Stretch
        {
            get
            {
                return stretch;
            }
            set
            {
                if (value != null && value != stretch && typeof(Stretch).GetConstants().ContainsKey(value))
                {
                    stretch = value;
                    OnPropertyChanged(nameof(Stretch));
                }
            }
        }
    }
}
