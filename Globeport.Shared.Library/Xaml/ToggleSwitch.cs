using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Globeport.Shared.Library.Interfaces;

namespace Globeport.Shared.Library.Xaml
{
    public class ToggleSwitch : Control, IToggleSwitch
    {
        public override string Type => nameof(ToggleSwitch);

        public ToggleSwitch()
        {
        }

        public override object Clone()
        {
            var element = new ToggleSwitch();
            element.CopyFrom(this);
            return element;
        }

        public override void CopyFrom(DependencyObject element)
        {
            var source = (ToggleSwitch)element;
            IsOn = source.IsOn;
            base.CopyFrom(source);
        }

        bool isOn;
        public bool IsOn
        {
            get
            {
                return isOn;
            }
            set
            {
                if (isOn != value)
                {
                    isOn = value;
                    OnPropertyChanged(nameof(IsOn));
                }
            }
        }
    }
}
