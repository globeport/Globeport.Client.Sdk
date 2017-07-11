using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Collections.Specialized;

using Globeport.Shared.Library.Interfaces;

namespace Globeport.Shared.Library.Xaml
{
    public class InkCanvas : FrameworkElement, IInkCanvas
    {
        public override string Type => nameof(InkCanvas);

        public InkCanvas()
        {
            isInputEnabled = true;
            penSize = "2,2";
        }

        public override object Clone()
        {
            var element = new InkCanvas();
            element.CopyFrom(this);
            return element;
        }

        public override void CopyFrom(DependencyObject element)
        {
            var source = (InkCanvas)element;
            IsInputEnabled = source.IsInputEnabled;
            Color = source.Color;
            PenSize = source.PenSize;
            base.CopyFrom(source);
        }

        public void Save(string id)
        {
            OnMethodCalled(new MethodEventArgs(nameof(Save), id));
        }

        public void Load(string id)
        {
            OnMethodCalled(new MethodEventArgs(nameof(Load), id));
        }

        public void Clear()
        {
            OnMethodCalled(new MethodEventArgs(nameof(Clear)));
        }


        bool isInputEnabled;
        public bool IsInputEnabled
        {
            get
            {
                return isInputEnabled;
            }
            set
            {
                if (isInputEnabled != value)
                {
                    isInputEnabled = value;
                    OnPropertyChanged(nameof(IsInputEnabled));
                }
            }
        }

        string color;
        public string Color
        {
            get
            {
                return color;
            }
            set
            {
                if (value != color)
                {
                    color = value;
                    OnPropertyChanged(nameof(Color));
                }
            }
        }

        string penSize;
        public string PenSize
        {
            get
            {
                return penSize;
            }
            set
            {
                if (value != penSize)
                {
                    penSize = value;
                    OnPropertyChanged(nameof(PenSize));
                }
            }
        }
    }
}
