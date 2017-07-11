using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Globeport.Shared.Library.Interfaces;

namespace Globeport.Shared.Library.Xaml
{
    public class HyperlinkButton : Button, IHyperlinkButton
    {
        public override string Type => nameof(HyperlinkButton);

        public HyperlinkButton()
        {
        }

        public override object Clone()
        {
            var element = new HyperlinkButton();
            element.CopyFrom(this);
            return element;
        }

        public override void CopyFrom(DependencyObject element)
        {
            var source = (HyperlinkButton)element;
            NavigateUri = source.NavigateUri;
            base.CopyFrom(source);
        }

        string navigateUri;
        public string NavigateUri
        {
            get
            {
                return navigateUri;
            }
            set
            {
                if (value != navigateUri)
                {
                    navigateUri = value;
                    OnPropertyChanged(nameof(NavigateUri));
                }
            }
        }
    }
}
