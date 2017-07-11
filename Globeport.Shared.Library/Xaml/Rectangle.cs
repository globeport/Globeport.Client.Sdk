using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Globeport.Shared.Library.Interfaces;

namespace Globeport.Shared.Library.Xaml
{
    public class Rectangle : Shape, IRectangle
    {
        public override string Type => nameof(Rectangle);

        public Rectangle()
        {
        }

        public override object Clone()
        {
            var element = new Rectangle();
            element.CopyFrom(this);
            return element;
        }

        public override void CopyFrom(DependencyObject element)
        {
            var source = (Rectangle)element;
            RadiusX = source.RadiusX;
            RadiusY = source.RadiusY;
            base.CopyFrom(source);
        }

        double radiusX;
        public double RadiusX
        {
            get
            {
                return radiusX;
            }
            set
            {
                if (radiusX!=value)
                {
                    radiusX = value;
                    OnPropertyChanged(nameof(RadiusX));
                }
            }
        }

        double radiusY;
        public double RadiusY
        {
            get
            {
                return radiusY;
            }
            set
            {
                if (radiusY != value)
                {
                    radiusY = value;
                    OnPropertyChanged(nameof(RadiusY));
                }
            }
        }
    }
}
