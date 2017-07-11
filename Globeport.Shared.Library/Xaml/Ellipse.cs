using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Globeport.Shared.Library.Xaml
{
    public class Ellipse : Shape
    {
        public override string Type => nameof(Ellipse);

        public Ellipse()
        {
        }

        public override object Clone()
        {
            var element = new Ellipse();
            element.CopyFrom(this);
            return element;
        }
    }
}
