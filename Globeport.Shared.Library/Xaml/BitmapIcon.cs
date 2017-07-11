using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Globeport.Shared.Library.Interfaces;

namespace Globeport.Shared.Library.Xaml
{
    public class BitmapIcon : Icon, IBitmapIcon
    {
        public override string Type => nameof(BitmapIcon);

        public BitmapIcon()
        {
        }

        public override object Clone()
        {
            var element = new BitmapIcon();
            element.CopyFrom(this);
            return element;
        }

        public override void CopyFrom(DependencyObject element)
        {
            var source = (BitmapIcon)element;
            UriSource = source.UriSource;
            base.CopyFrom(source);
        }

        string uriSource;
        public string UriSource
        {
            get
            {
                return uriSource;
            }
            set
            {
                if (value!=uriSource)
                {
                    uriSource = value;
                    OnPropertyChanged(nameof(UriSource));
                }
            }
        } 
    }
}
