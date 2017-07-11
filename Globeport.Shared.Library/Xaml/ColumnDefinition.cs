using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Globeport.Shared.Library.Interfaces;

namespace Globeport.Shared.Library.Xaml
{
    public class ColumnDefinition : DependencyObject, IColumnDefinition
    {
        public override string Type => nameof(ColumnDefinition);

        public ColumnDefinition()
        {
        }

        public ColumnDefinition(string width)
        {
            Width = width;
        }

        public override object Clone()
        {
            var element = new ColumnDefinition();
            element.CopyFrom(this);
            return element;
        }

        public override void CopyFrom(DependencyObject element)
        {
            var source = (ColumnDefinition)element;
            Width = source.Width;
            base.CopyFrom(source);
        }

        string width;
        public string Width
        {
            get
            {
                return width;
            }
            set
            {
                if (width != value)
                {
                    width = value;
                    OnPropertyChanged(nameof(Width));
                }
            }
        }
    }
}
