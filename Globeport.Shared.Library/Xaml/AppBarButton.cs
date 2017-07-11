using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Globeport.Shared.Library.Xaml
{
    public class AppBarButton : Button
    {
        public override string Type => nameof(AppBarButton);

        public AppBarButton()
        {
        }

        public override object Clone()
        {
            var element = new AppBarButton();
            element.CopyFrom(this);
            return element;
        }
    }
}
