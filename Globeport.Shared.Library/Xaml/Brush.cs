using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Globeport.Shared.Library.Xaml
{
    public class Brush : DependencyObject
    {
        public override string Type => nameof(Brush);

        public override object Clone()
        {
            var element = new Brush();
            element.CopyFrom(this);
            return element;
        }
    }
}
