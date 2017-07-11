using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Globeport.Shared.Library.Interfaces;

namespace Globeport.Shared.Library.Xaml
{
    public class DatePicker : Control, IDatePicker
    {
        public override string Type => nameof(DatePicker);

        public DatePicker()
        {
            dayVisible = true;
            monthVisible = true;
            yearVisible = true;
        }

        public override object Clone()
        {
            var element = new DatePicker();
            element.CopyFrom(this);
            return element;
        }

        public override void CopyFrom(DependencyObject element)
        {
            var source = (DatePicker)element;
            Date = source.Date;
            DayVisible = source.DayVisible;
            MonthVisible = source.MonthVisible;
            YearVisible = source.YearVisible;
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

        bool dayVisible;
        public bool DayVisible
        {
            get
            {
                return dayVisible;
            }
            set
            {
                if (value!=dayVisible)
                {
                    dayVisible = value;
                    OnPropertyChanged(nameof(DayVisible));
                }
            }
        }

        bool monthVisible;
        public bool MonthVisible
        {
            get
            {
                return monthVisible;
            }
            set
            {
                if (value != monthVisible)
                {
                    monthVisible = value;
                    OnPropertyChanged(nameof(MonthVisible));
                }
            }
        }

        bool yearVisible;
        public bool YearVisible
        {
            get
            {
                return yearVisible;
            }
            set
            {
                if (value != yearVisible)
                {
                    yearVisible = value;
                    OnPropertyChanged(nameof(YearVisible));
                }
            }
        }
    }
}
