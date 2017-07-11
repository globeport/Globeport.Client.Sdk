using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Globeport.Shared.Library.Interfaces;
using Globeport.Shared.Library.Extensions;

namespace Globeport.Shared.Library.Xaml
{
    public class PasswordBox : Control, IPasswordBox
    {
        public override string Type => nameof(PasswordBox);

        public PasswordBox()
        {
        }

        public override object Clone()
        {
            var element = new PasswordBox();
            element.CopyFrom(this);
            return element;
        }

        public override void CopyFrom(DependencyObject element)
        {
            var source = (PasswordBox)element;
            KeyDown = source.KeyDown;
            PasswordRevealMode = source.PasswordRevealMode;
            Password = source.Password;
            InputScope = source.InputScope;
            MaxLength = source.MaxLength;
            base.CopyFrom(source);
        }

        string passwordRevealMode;
        public string PasswordRevealMode
        {
            get
            {
                return passwordRevealMode;
            }
            set
            {
                if (value != null && passwordRevealMode != value && typeof(PasswordRevealMode).GetConstants().ContainsKey(value))
                {
                    passwordRevealMode = value;
                    OnPropertyChanged(nameof(PasswordRevealMode));
                }
            }
        }

        string password;
        public string Password
        {
            get
            {
                return password;
            }
            set
            {
                if (password != value)
                {
                    password = value;
                    OnPropertyChanged(nameof(Password));
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
    }
}
