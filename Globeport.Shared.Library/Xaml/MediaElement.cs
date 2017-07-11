using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Globeport.Shared.Library.Interfaces;

namespace Globeport.Shared.Library.Xaml
{
    public class MediaElement : FrameworkElement, IMediaElement
    {
        public override string Type => nameof(MediaElement);

        public MediaElement()
        {
        }

        public override object Clone()
        {
            var element = new MediaElement();
            element.CopyFrom(this);
            return element;
        }

        public override void CopyFrom(DependencyObject element)
        {
            var source = (MediaElement)element;
            Source = source.Source;
            base.CopyFrom(source);
        }

        string source;
        public string Source
        {
            get
            {
                return source;
            }
            set
            {
                if (source!=value)
                {
                    source = value;
                    OnPropertyChanged(nameof(Source));
                }
            }
        }
    }
}
