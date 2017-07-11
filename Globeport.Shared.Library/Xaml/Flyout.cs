using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

using Portable.Xaml.Markup;

using Globeport.Shared.Library.Interfaces;
using Globeport.Shared.Library.Extensions;

namespace Globeport.Shared.Library.Xaml
{
    [ContentProperty("Content")]
    public class Flyout : DependencyObject, IFlyout
    {
        public override string Type => nameof(Flyout);
        public string Opening { get; set; }
        public string Opened { get; set; }
        public string Closing { get; set; }
        public string Closed { get; set; }

        public Flyout()
        {
        }

        public override List<DependencyObject> GetElements()
        {
            var elements = base.GetElements();
            if (Content != null) elements.AddRange(Content.GetElements());
            return elements;
        }

        public void Hide()
        {
            OnMethodCalled(new MethodEventArgs(nameof(Hide)));
        }

        public override object Clone()
        {
            var element = new Flyout();
            element.CopyFrom(this);
            return element;
        }

        public override void CopyFrom(DependencyObject element)
        {
            var source = (Flyout)element;
            Opening = source.Opening;
            Opened = source.Opened;
            Closing = source.Closing;
            Closed = source.Closed;
            Placement = source.Placement;
            Content = (FrameworkElement)source.Content?.Clone();
            base.CopyFrom(source);
        }

        string placement;
        public string Placement
        {
            get
            {
                return placement;
            }
            set
            {
                if (value != null && placement !=value && typeof(FlyoutPlacementMode).GetConstants().ContainsKey(value))
                {
                    placement = value;
                    OnPropertyChanged(nameof(Placement));
                }
            }
        }

        FrameworkElement content;
        public FrameworkElement Content
        {
            get
            {
                return content;
            }
            set
            {
                if (value != content)
                {
                    content = value;
                    OnPropertyChanged(nameof(Content));
                }
            }
        }
    }
}
