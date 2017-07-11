using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Globeport.Shared.Library.Interfaces;

namespace Globeport.Shared.Library.Xaml
{
    public class CalendarDatePicker : Control, ICalendarDatePicker
    {
        public override string Type => nameof(CalendarDatePicker);

        public CalendarDatePicker()
        { 
        }

        public override object Clone()
        {
            var element = new CalendarDatePicker();
            element.CopyFrom(this);
            return element;
        }

        public override void CopyFrom(DependencyObject element)
        {
            var source = (CalendarDatePicker)element;
            Date = source.Date;
            base.CopyFrom(source);
        }

        string date;
        public string Date
        {
            get
            {
                return date;
            }
            set
            {
                if (date != value)
                {
                    date = value;
                    base.OnPropertyChanged(nameof(Date));
                }
            }
        }
    }
}
