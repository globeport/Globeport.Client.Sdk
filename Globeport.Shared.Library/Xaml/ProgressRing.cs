using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Globeport.Shared.Library.Interfaces;

namespace Globeport.Shared.Library.Xaml
{
    public class ProgressRing : Control, IProgressRing
    {
        public override string Type => nameof(ProgressRing);

        public ProgressRing()
        {
        }

        public override object Clone()
        {
            var element = new ProgressRing();
            element.CopyFrom(this);
            return element;
        }

        public override void CopyFrom(DependencyObject element)
        {
            var source = (ProgressRing)element;
            IsActive = source.IsActive;
            base.CopyFrom(source);
        }

        bool isActive;
        public bool IsActive
        {
            get
            {
                return isActive;
            }
            set
            {
                if (isActive!=value)
                {
                    isActive = value;
                    OnPropertyChanged(nameof(IsActive));
                }
            }
        }
    }
}
