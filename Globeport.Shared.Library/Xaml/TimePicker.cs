using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Globeport.Shared.Library.Interfaces;

namespace Globeport.Shared.Library.Xaml
{
    public class TimePicker : Control, ITimePicker
    {
        public override string Type => nameof(TimePicker);

        public TimePicker()
        {
        }

        public override object Clone()
        {
            var element = new TimePicker();
            element.CopyFrom(this);
            return element;
        }

        public override void CopyFrom(DependencyObject element)
        {
            var source = (TimePicker)element;
            Time = source.Time;
            base.CopyFrom(source);
        }

        string time;
        public string Time
        {
            get
            {
                return time;
            }
            set
            {
                if (time != value)
                {
                    time = value;
                    base.OnPropertyChanged(nameof(Time));
                }
            }
        }
    }
}
