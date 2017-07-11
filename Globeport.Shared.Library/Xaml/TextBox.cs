using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Globeport.Shared.Library.Interfaces;
using Globeport.Shared.Library.Extensions;

namespace Globeport.Shared.Library.Xaml
{
    public class TextBox : Control, ITextBox
    {
        public override string Type => nameof(TextBox);

        public string TextChanged { get; set; }

        public TextBox()
        {
            IsColorFontEnabled = true;
            IsSpellCheckEnabled = true;
            IsTextPredictionEnabled = true;
        }

        public override object Clone()
        {
            var element = new TextBox();
            element.CopyFrom(this);
            return element;
        }

        public override void CopyFrom(DependencyObject element)
        {
            var source = (TextBox)element;
            AcceptsReturn = source.AcceptsReturn;
            InputScope = source.InputScope;
            AcceptsReturn = source.AcceptsReturn;
            IsColorFontEnabled = source.IsColorFontEnabled;
            MaxLength = source.MaxLength;
            IsReadOnly = source.IsReadOnly;
            Text = source.Text;
            TextWrapping = source.TextWrapping;
            TextAlignment = source.TextAlignment;
            IsSpellCheckEnabled = source.IsSpellCheckEnabled;
            IsTextPredictionEnabled = source.IsTextPredictionEnabled;
            PlaceholderText = source.PlaceholderText;
            TextChanged = source.TextChanged;
            base.CopyFrom(source);
        }

        bool acceptsReturn;
        public bool AcceptsReturn
        {
            get
            {
                return acceptsReturn;
            }
            set
            {
                if (acceptsReturn != value)
                {
                    acceptsReturn = value;
                    OnPropertyChanged(nameof(AcceptsReturn));
                }
            }
        }

        string inputScope;
        public string InputScope
        {
            get
            {
                return inputScope;
            }
            set
            {
                if (value != null && inputScope != value && typeof(InputScopeNameValue).GetConstants().ContainsKey(value))
                {
                    inputScope = value;
                    OnPropertyChanged(nameof(InputScope));
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

        int maxLength;
        public int MaxLength
        {
            get
            {
                return maxLength;
            }
            set
            {
                if (maxLength != value)
                {
                    maxLength = value;
                    OnPropertyChanged(nameof(MaxLength));
                }
            }
        }

        bool isReadOnly;
        public bool IsReadOnly
        {
            get
            {
                return isReadOnly;
            }
            set
            {
                if (isReadOnly != value)
                {
                    isReadOnly = value;
                    OnPropertyChanged(nameof(IsReadOnly));
                }
            }
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

        string placeholderText;
        public string PlaceholderText
        {
            get
            {
                return placeholderText;
            }
            set
            {
                if (placeholderText != value)
                {
                    placeholderText = value ?? string.Empty;
                    OnPropertyChanged(nameof(PlaceholderText));
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

        bool isSpellCheckEnabled;
        public bool IsSpellCheckEnabled
        {
            get
            {
                return isSpellCheckEnabled;
            }
            set
            {
                if (value != isSpellCheckEnabled)
                {
                    isSpellCheckEnabled = value;
                    OnPropertyChanged(nameof(IsSpellCheckEnabled));
                }
            }
        }

        bool isTextPredictionEnabled;
        public bool IsTextPredictionEnabled
        {
            get
            {
                return isTextPredictionEnabled;
            }
            set
            {
                if (value != isTextPredictionEnabled)
                {
                    isTextPredictionEnabled = value;
                    OnPropertyChanged(nameof(IsTextPredictionEnabled));
                }
            }
        }
    }
}
