using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Globeport.Shared.Library.Collections;

namespace Globeport.Shared.Library.Xaml
{
    public class FrameworkElements : ObservableList<FrameworkElement>
    {
        public FrameworkElements()
        {
        }

        public FrameworkElements(IEnumerable<FrameworkElement> elements) 
            : base(elements)
        {
        }
    }
}
