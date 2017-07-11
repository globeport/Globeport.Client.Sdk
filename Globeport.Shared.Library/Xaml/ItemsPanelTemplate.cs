using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Globeport.Shared.Library.Xaml
{
    public class ItemsPanelTemplate : FrameworkTemplate
    {
        public override string Type => nameof(ItemsPanelTemplate);

        public ItemsPanelTemplate()
        {
        }

        public override object Clone()
        {
            var element = new ItemsPanelTemplate();
            element.CopyFrom(this);
            return element;
        }
    }
}
