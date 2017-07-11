using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Globeport.Shared.Library.Interfaces;

namespace Globeport.Shared.Library.Xaml
{
    public class ProgressBar : Control, IProgressBar
    {
        public override string Type => nameof(ProgressBar);

        public ProgressBar()
        {
            maximum = 1;
        }

        public override object Clone()
        {
            var element = new ProgressBar();
            element.CopyFrom(this);
            return element;
        }

        public override void CopyFrom(DependencyObject element)
        {
            var source = (ProgressBar)element;
            IsIndeterminate = source.IsIndeterminate;
            Value = source.Value;
            Maximum = source.Maximum;
            Minimum = source.Minimum;
            base.CopyFrom(source);
        }

        bool isIndeterminate;
        public bool IsIndeterminate
        {
            get
            {
                return isIndeterminate;
            }
            set
            {
                if (isIndeterminate != value)
                {
                    isIndeterminate = value;
                    OnPropertyChanged(nameof(IsIndeterminate));
                }
            }
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
