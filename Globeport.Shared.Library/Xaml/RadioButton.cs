using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Globeport.Shared.Library.Interfaces;

namespace Globeport.Shared.Library.Xaml
{
    public class RadioButton : ToggleButton, IRadioButton
    {
        public override string Type => nameof(RadioButton);

        public RadioButton()
        {
        }

        public override object Clone()
        {
            var element = new RadioButton();
            element.CopyFrom(this);
            return element;
        }

        public override void CopyFrom(DependencyObject element)
        {
            var source = (RadioButton)element;
            GroupName = source.GroupName;
            base.CopyFrom(source);
        }

        string groupName;
        public string GroupName
        {
            get
            {
                return groupName;
            }
            set
            {
                if (groupName != value && value != null)
                {
                    groupName = value;
                    OnPropertyChanged(nameof(GroupName));
                }
            }
        }
    }
}
