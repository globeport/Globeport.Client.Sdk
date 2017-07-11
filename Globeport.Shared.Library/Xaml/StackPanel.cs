using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Globeport.Shared.Library.Interfaces;
using Globeport.Shared.Library.Extensions;

namespace Globeport.Shared.Library.Xaml
{
    public class StackPanel : Panel, IStackPanel
    {
        public override string Type => nameof(StackPanel);

        public StackPanel()  
        {
        }

        public override object Clone()
        {
            var element = new StackPanel();
            element.CopyFrom(this);
            return element;
        }

        public override void CopyFrom(DependencyObject element)
        {
            var source = (StackPanel)element;
            Orientation = source.Orientation;
            BorderThickness = source.BorderThickness;
            BorderBrush = source.BorderBrush;
            CornerRadius = source.CornerRadius;
            Padding = source.Padding;
            base.CopyFrom(source);
        }

        string orientation;
        public string Orientation 
        {
            get
            {
                return orientation;
            }
            set
            {
                if (value != null && orientation != value && typeof(Orientation).GetConstants().ContainsKey(value))
                {
                    orientation = value;
                    OnPropertyChanged(nameof(Orientation));
                }
            }
        }

        string borderThickness;
        public string BorderThickness
        {
            get
            {
                return borderThickness;
            }
            set
            {
                if (value != borderThickness)
                {
                    borderThickness = value;
                    OnPropertyChanged(nameof(BorderThickness));
                }
            }
        }

        string borderBrush;
        public string BorderBrush
        {
            get
            {
                return borderBrush;
            }
            set
            {
                if (value != borderBrush)
                {
                    borderBrush = value;
                    OnPropertyChanged(nameof(BorderBrush));
                }
            }
        }

        string cornerRadius;
        public string CornerRadius
        {
            get
            {
                return cornerRadius;
            }
            set
            {
                if (value != cornerRadius)
                {
                    cornerRadius = value;
                    OnPropertyChanged(nameof(CornerRadius));
                }
            }
        }

        string padding;
        public string Padding
        {
            get
            {
                return padding;
            }
            set
            {
                if (padding != value)
                {
                    padding = value;
                    OnPropertyChanged(nameof(Padding));
                }
            }
        }
    }
}
