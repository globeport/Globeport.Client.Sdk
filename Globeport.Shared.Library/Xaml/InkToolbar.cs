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
    public class InkToolbar : Control, IInkToolbar
    {
        public override string Type => nameof(InkToolbar);

        public InkToolbar()
        {
        }

        public override object Clone()
        {
            var element = new InkToolbar();
            element.CopyFrom(this);
            return element;
        }

        public override void CopyFrom(DependencyObject element)
        {
            var source = (InkToolbar)element;
            TargetInkCanvas = source.targetInkCanvas;
            base.CopyFrom(source);
        }

        string targetInkCanvas;
        public string TargetInkCanvas
        {
            get
            {
                return targetInkCanvas;
            }
            set
            {
                if (value!=null)
                {
                    targetInkCanvas = value;
                    OnPropertyChanged(nameof(TargetInkCanvas));
                }
            }
        }
    }
}
