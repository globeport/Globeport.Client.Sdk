using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Globeport.Shared.Library.Interfaces;
using Globeport.Shared.Library.Extensions;

namespace Globeport.Shared.Library.Xaml
{
    public class FontIcon : Icon, IFontIcon
    {
        public override string Type => nameof(FontIcon);

        public FontIcon()
        {
            fontSize = double.NaN;
        }

        public override object Clone()
        {
            var element = new FontIcon();
            element.CopyFrom(this);
            return element;
        }

        public override void CopyFrom(DependencyObject element)
        {
            var source = (FontIcon)element;
            Glyph = source.Glyph;
            FontSize = source.FontSize;
            FontStyle = source.FontStyle;
            FontWeight = source.FontWeight;
            FontFamily = source.FontFamily;
            base.CopyFrom(source);
        }

        string glyph;
        public string Glyph
        {
            get
            {
                return glyph;
            }
            set
            {
                if (value!= glyph)
                {
                    glyph = value;
                    OnPropertyChanged(nameof(Glyph));
                }
            }
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
    }
}
