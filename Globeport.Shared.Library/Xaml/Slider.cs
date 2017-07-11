using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Globeport.Shared.Library.Interfaces;

namespace Globeport.Shared.Library.Xaml
{
    public class Slider : Control, ISlider
    {
        public override string Type => nameof(Slider);

        public Slider()
        { 
            maximum = 1;
        }

        public override object Clone()
        {
            var element = new Slider();
            element.CopyFrom(this);
            return element;
        }

        public override void CopyFrom(DependencyObject element)
        {
            var source = (Slider)element;
            Value = source.Value;
            Maximum = source.Maximum;
            Minimum = source.Minimum;
            base.CopyFrom(source);
        }

        double value;
        public double Value
        {
            get
            {
                return value;
            }
            set
            {
                if (this.value != value)
                {
                    this.value = value;
                    OnPropertyChanged(nameof(Value));
                }
            }
        }

        double maximum;
        public double Maximum
        {
            get
            {
                return maximum;
            }
            set
            {
                if (maximum != value)
                {
                    maximum = value;
                    OnPropertyChanged(nameof(Maximum));
                }
            }
        }

        double minimum;
        public double Minimum
        {
            get
            {
                return minimum;
            }
            set
            {
                if (minimum != value)
                {
                    minimum = value;
                    OnPropertyChanged(nameof(Minimum));
                }
            }
        }
    }
}
