using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Globeport.Shared.Library.Interfaces;

namespace Globeport.Shared.Library.Xaml
{
    public class Button : ContentControl, IButton
    {
        public override string Type => nameof(Button);

        public Button()
        {
        }

        public override List<DependencyObject> GetElements()
        {
            var elements = base.GetElements();
            if (Flyout != null)
            {
                elements.AddRange(Flyout.GetElements());
            }
            return elements;
        }

        public override object Clone()
        {
            var element = new Button();
            element.CopyFrom(this);
            return element;
        }

        public override void CopyFrom(DependencyObject element)
        {
            var source = (Button)element;
            Flyout = (Flyout)source.Flyout?.Clone();
            base.CopyFrom(source);
        }

        Flyout flyout;
        public Flyout Flyout
        {
            get
            {
                return flyout;
            }
            set
            {
                if (flyout != value)
                {
                    flyout = value;
                    OnPropertyChanged(nameof(Flyout));
                }
            }
        }
    }
}
