using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Globeport.Shared.Library.Interfaces;

namespace Globeport.Shared.Library.Xaml
{
    public class ToggleButton : ContentControl, IToggleButton
    {
        public override string Type => nameof(ToggleButton);

        public ToggleButton()
        {
        }

        public override void CopyFrom(DependencyObject element)
        {
            var source = (ToggleButton)element;
            IsChecked = source.IsChecked;
            base.CopyFrom(source);
        }

        public override object Clone()
        {
            var element = new ToggleButton();
            element.CopyFrom(this);
            return element;
        }

        bool isChecked;
        public bool IsChecked
        {
            get
            {
                return isChecked;
            }
            set
            {
                if (isChecked != value)
                {
                    isChecked = value;
                    OnPropertyChanged(nameof(IsChecked));
                }
            }
        }
    }
}
