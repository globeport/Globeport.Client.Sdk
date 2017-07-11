using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Portable.Xaml.Markup;

using Globeport.Shared.Library.Interfaces;
using Globeport.Shared.Library.Validation;

namespace Globeport.Shared.Library.Xaml
{
    [ContentProperty("Content")]
    public class ContentControl : Control, IContentControl
    {
        public override string Type => nameof(ContentControl);
        public string EventRaised { get; set; }

        public ContentControl()
        {
        }

        public override List<DependencyObject> GetElements()
        {
            var elements = base.GetElements();
            if (Content is FrameworkElement) elements.AddRange(((FrameworkElement)Content).GetElements());
            return elements;
        }

        public override object Clone()
        {
            var element = new ContentControl();
            element.CopyFrom(this);
            return element;
        }

        public override void CopyFrom(DependencyObject element)
        {
            var source = (ContentControl)element;
            if (source.content is FrameworkElement) 
            {
                Content = ((FrameworkElement)source.Content).Clone();
            }
            else
            {
                Content = source.Content;
            }
            Control = source.Control;
            Element = source.Element;
            EventRaised = source.EventRaised;
            base.CopyFrom(source);
        }

        object content;
        public object Content
        {
            get
            {
                return content;
            }
            set
            {
                if (value!=content && (value==null || value is FrameworkElement || value is string))
                {
                    Control = null;
                    Element = null;
                    content = value;
                    OnPropertyChanged(nameof(Content));
                }
            }
        }

        string control;
        public string Control
        {
            get
            {
                return control;
            }
            set
            {
                if (control != value)
                {
                    Content = null;
                    Element = null;
                    control = value;
                    OnPropertyChanged(nameof(Control));
                }
            }
        }


        string element;
        public string Element
        {
            get
            {
                return element;
            }
            set
            {
                if (element != value && (value == null || Validators.IsValidId(value)))
                {
                    Content = null;
                    Control = null;
                    element = value;
                    OnPropertyChanged(nameof(Element));
                }
            }
        }
    }
}
