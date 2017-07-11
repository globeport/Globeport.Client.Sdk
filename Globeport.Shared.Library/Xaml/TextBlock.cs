using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Globeport.Shared.Library.Interfaces;
using Globeport.Shared.Library.Extensions;

namespace Globeport.Shared.Library.Xaml
{
    public class TextBlock : FrameworkElement, ITextBlock
    {
        public override string Type => nameof(TextBlock);

        public TextBlock()
        {
            fontSize = double.NaN;
            IsColorFontEnabled = true;
        }

        public override object Clone()
        {
            var element = new TextBlock();
            element.CopyFrom(this);
            return element;
        }

        public override void CopyFrom(DependencyObject element)
        {
            var source = (TextBlock)element;
            Text = source.Text;
            TextWrapping = source.TextWrapping;
            TextAlignment = source.TextAlignment;
            FontSize = source.FontSize;
            FontStyle = source.FontStyle;
            FontWeight = source.FontWeight;
            FontFamily = source.FontFamily;
            IsColorFontEnabled = source.IsColorFontEnabled;
            Foreground = source.Foreground;
            Padding = source.Padding;
            MentionsUrl = source.MentionsUrl;
            base.CopyFrom(source);
        }

        string text;
        public string Text
        {
            get
            {
                return text;
            }
            set
            {
                if (text != value)
                {
                    text = value ?? string.Empty;
                    OnPropertyChanged(nameof(Text));
                }
            }
        }

        string textWrapping;
        public string TextWrapping
        {
            get
            {
                return textWrapping;
            }
            set
            {
                if (value != null && textWrapping != value && typeof(TextWrapping).GetConstants().ContainsKey(value))
                {
                    textWrapping = value;
                    OnPropertyChanged(nameof(TextWrapping));
                }
            }
        }

        string textAlignment;
        public string TextAlignment
        {
            get
            {
                return textAlignment;
            }
            set
            {
                if (value != null && textAlignment != value && typeof(TextAlignment).GetConstants().ContainsKey(value))
                {
                    textAlignment = value;
                    OnPropertyChanged(nameof(TextAlignment));
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

        bool isColorFontEnabled;
        public bool IsColorFontEnabled
        {
            get
            {
                return isColorFontEnabled;
            }
            set
            {
                if (isColorFontEnabled != value)
                {
                    isColorFontEnabled = value;
                    OnPropertyChanged(nameof(IsColorFontEnabled));
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

        string mentionsUrl;
        public string MentionsUrl
        {
            get
            {
                return mentionsUrl;
            }
            set
            {
                if (mentionsUrl != value)
                {
                    mentionsUrl = value;
                    OnPropertyChanged(nameof(MentionsUrl));
                }
            }
        }
    }
}
