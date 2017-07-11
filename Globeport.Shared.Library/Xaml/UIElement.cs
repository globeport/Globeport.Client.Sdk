using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Runtime.Serialization;
using System.ComponentModel;
using System.Runtime.CompilerServices;

using Globeport.Shared.Library.Interfaces;
using Globeport.Shared.Library.Extensions;

namespace Globeport.Shared.Library.Xaml
{
    public class UIElement : DependencyObject, IUIElement
    {
        public override string Type => nameof(UIElement);
        public string Tapped { get; set; }
        public string RightTapped { get; set; }
        public string GotFocus { get; set; }
        public string LostFocus { get; set; }
        public string KeyUp { get; set; }
        public string KeyDown { get; set; }

        public UIElement()
        {
            opacity = 1;
            isHitTestVisible = true;
        }

        public override List<DependencyObject> GetElements()
        {
            var elements = base.GetElements();
            if (ContextFlyout != null)
            {
                elements.AddRange(ContextFlyout.GetElements());
            }
            return elements;
        }

        public override object Clone()
        {
            var element = new UIElement();
            element.CopyFrom(this);
            return element;
        }

        public override void CopyFrom(DependencyObject element)
        {
            var source = (UIElement)element;
            Tapped = source.Tapped;
            RightTapped = source.RightTapped;
            GotFocus = source.GotFocus;
            LostFocus = source.LostFocus;
            KeyDown = source.KeyDown;
            KeyUp = source.KeyUp;
            Visibility = source.Visibility;
            Opacity = source.Opacity;
            IsHitTestVisible = source.IsHitTestVisible;;
            ContextFlyout = (Flyout)source.ContextFlyout?.Clone();
            base.CopyFrom(source);
        }

        string visibility;
        public string Visibility
        {
            get
            {
                return visibility;
            }
            set
            {
                if (value != null && visibility!=value && typeof(Visibility).GetConstants().ContainsKey(value))
                {
                    visibility = value;
                    OnPropertyChanged(nameof(Visibility));
                }
            }
        }


        double opacity;
        public double Opacity
        {
            get
            {
                return opacity;
            }
            set
            {
                if (opacity != value && value >= 0)
                {
                    opacity = value;
                    OnPropertyChanged(nameof(Opacity));
                }
            }
        }

        bool isHitTestVisible;
        public bool IsHitTestVisible
        {
            get
            {
                return isHitTestVisible;
            }
            set
            {
                if (isHitTestVisible != value)
                {
                    isHitTestVisible = value;
                    OnPropertyChanged(nameof(IsHitTestVisible));
                }
            }
        }

        Flyout contextFlyout;
        public Flyout ContextFlyout
        {
            get
            {
                return contextFlyout;
            }
            set
            {
                if (contextFlyout != value)
                {
                    contextFlyout = value;
                    OnPropertyChanged(nameof(ContextFlyout));
                }
            }
        }
    }
}
