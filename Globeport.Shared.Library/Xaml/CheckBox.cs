using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Globeport.Shared.Library.Interfaces;

namespace Globeport.Shared.Library.Xaml
{
    public class CheckBox : ToggleButton, ICheckBox
    {
        public override string Type => nameof(CheckBox);

        public CheckBox()
        {
        }

        public override object Clone()
        {
            var element = new CheckBox();
            element.CopyFrom(this);
            return element;
        }
    }
}
