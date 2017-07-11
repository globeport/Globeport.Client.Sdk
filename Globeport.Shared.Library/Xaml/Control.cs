using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Globeport.Shared.Library.Interfaces;
using Globeport.Shared.Library.Extensions;

namespace Globeport.Shared.Library.Xaml
{
    public class Control : FrameworkElement, IControl
    {
        public override string Type => nameof(Control);

        public Control()
        {
            fontSize = double.NaN;
            isEnabled = true;
        }

        public override List<DependencyObject> GetElements()
        {
            var elements = base.GetElements();
            if (Background is ImageBrush)
            {
                elements.AddRange(((ImageBrush)Background).GetElements());
            }
            return elements;
        }

        public void Focus()
        {
            OnMethodCalled(new MethodEventArgs(nameof(Focus)));
        }

        public override object Clone()
        {
            var element = new Control();
            element.CopyFrom(this);
            return element;
        }

        public override void CopyFrom(DependencyObject element)
        {
            var source = (Control)element;
            FontSize = source.FontSize;
            FontStyle = source.FontStyle;
            FontWeight = source.FontWeight;
            FontFamily = source.FontFamily;
            Foreground = source.Foreground;
            if (source.Background is ImageBrush)
            {
                Background = ((ImageBrush)source.Background).Clone();
            }
            else
            {
                Background = source.Background;
            }
            HorizontalContentAlignment = source.HorizontalContentAlignment;
            VerticalContentAlignment = source.VerticalContentAlignment;
            BorderThickness = source.BorderThickness;
            BorderBrush = source.BorderBrush;
            Padding = source.Padding;
            IsEnabled = source.IsEnabled;
            base.CopyFrom(source);
        }

        double fontSize;
        public double FontSize
        {
            get
            {
                return fontSize;
            }
            set
            {
                if (fontSize != value && value >= 0)
                {
                    fontSize = value;
                    OnPropertyChanged(nameof(FontSize));
                }
            }
        }

        string fontStyle;
        public string FontStyle
        {
            get
            {
                return fontStyle;
            }
            set
            {
                if (value != null && fontStyle != value && typeof(FontStyle).GetConstants().ContainsKey(value))
                {
                    fontStyle = value;
                    OnPropertyChanged(nameof(FontStyle));
                }
            }
        }

        string fontWeight;
        public string FontWeight
        {
            get
            {
                return fontWeight;
            }
            set
            {
                if (value != null && fontWeight != value && typeof(FontWeight).GetConstants().ContainsKey(value))
                {
                    fontWeight = value;
                    OnPropertyChanged(nameof(FontWeight));
                }
            }
        }

        string fontFamily;
        public string FontFamily
        {
            get
            {
                return fontFamily;
            }
            set
            {
                if (fontFamily != value)
                {
                    fontFamily = value;
                    OnPropertyChanged(nameof(FontFamily));
                }
            }
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

        object background;
        public object Background
        {
            get
            {
                return background;
            }
            set
            {
                if (value != background & (value == null || value is ImageBrush || value is string))
                {
                    background = value;
                    OnPropertyChanged(nameof(Background));
                }
            }
        }

        string horizontalContentAlignment;
        public string HorizontalContentAlignment
        {
            get
            {
                return horizontalContentAlignment;
            }
            set
            {
                if (value != null && value != horizontalContentAlignment && typeof(HorizontalAlignment).GetConstants().ContainsKey(value))
                {
                    horizontalContentAlignment = value;
                    OnPropertyChanged(nameof(HorizontalContentAlignment));
                }
            }
        }

        string verticalContentAlignment;
        public string VerticalContentAlignment
        {
            get
            {
                return verticalContentAlignment;
            }
            set
            {
                if (value != null && value != verticalContentAlignment && typeof(VerticalAlignment).GetConstants().ContainsKey(value))
                {
                    verticalContentAlignment = value;
                    OnPropertyChanged(nameof(VerticalContentAlignment));
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

        bool isEnabled;
        public bool IsEnabled
        {
            get
            {
                return isEnabled;
            }
            set
            {
                if (isEnabled != value)
                {
                    isEnabled = value;
                    OnPropertyChanged(nameof(IsEnabled));
                }
            }
        }
    }
}
