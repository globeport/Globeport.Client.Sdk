using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Globeport.Shared.Library.Interfaces;

namespace Globeport.Shared.Library.Xaml
{
    public class Icon : FrameworkElement, IIcon
    {
        public override string Type => nameof(Icon);

        public Icon()
        {
        }

        public override object Clone()
        {
            var element = new Icon();
            element.CopyFrom(this);
            return element;
        }

        public override void CopyFrom(DependencyObject element)
        {
            var source = (Icon)element;
            Foreground = source.Foreground;
            base.CopyFrom(source);
        }

        string foreground;
        public string Foreground
        {
            get
            {
                return foreground;
            }
            set
            {
                if (foreground != value)
                {
                    foreground = value;
                    OnPropertyChanged(nameof(Foreground));
                }
            }
        }
    }
}
