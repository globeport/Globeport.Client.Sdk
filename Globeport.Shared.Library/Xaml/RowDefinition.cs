using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Globeport.Shared.Library.Interfaces;

namespace Globeport.Shared.Library.Xaml
{ 
    public class RowDefinition : DependencyObject, IRowDefinition
    {
        public override string Type => nameof(RowDefinition);

        public RowDefinition()
        {
        }

        public RowDefinition(string height)
        {
            Height = height;
        }

        public override object Clone()
        {
            var element = new RowDefinition();
            element.CopyFrom(this);
            return element;
        }

        public override void CopyFrom(DependencyObject element)
        {
            var source = (RowDefinition)element;
            Height = source.Height;
            base.CopyFrom(source);
        }


        string height;
        public string Height
        {
            get
            {
                return height;
            }
            set
            {
                if (height != value)
                {
                    height = value;
                    OnPropertyChanged(nameof(Height));
                }
            }
        }
    }
}
