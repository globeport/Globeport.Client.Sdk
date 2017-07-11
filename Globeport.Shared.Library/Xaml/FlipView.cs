using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace Globeport.Shared.Library.Xaml
{
    public class FlipView : Selector
    {
        public override string Type => nameof(FlipView);

        public FlipView()
        {
        }

        public override object Clone()
        {
            var element = new FlipView();
            element.CopyFrom(this);
            return element;
        }
    }
}
