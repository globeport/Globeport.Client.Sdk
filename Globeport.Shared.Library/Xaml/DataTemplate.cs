using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Globeport.Shared.Library.Xaml
{
    public class DataTemplate : FrameworkTemplate
    {
        public override string Type => nameof(DataTemplate);

        public DataTemplate()
        {
        }

        public override object Clone()
        {
            var element = new DataTemplate();
            element.CopyFrom(this);
            return element;
        }
    }
}
