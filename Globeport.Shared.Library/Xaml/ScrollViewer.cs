using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Globeport.Shared.Library.Extensions;
using Globeport.Shared.Library.Interfaces;

namespace Globeport.Shared.Library.Xaml
{ 
    public class ScrollViewer : ContentControl, IScrollViewer
    {
        public override string Type => nameof(ScrollViewer);

        public ScrollViewer()
        {
        }

        public static void SetHorizontalScrollBarVisibility(FrameworkElement instance, string value)
        {
            if (value != null && typeof(ScrollBarVisibility).GetConstants().ContainsKey(value))
            {
                instance.SetValue(ScrollViewerProperties.HorizontalScrollBarVisibility, value);
            }
        }

        public static void SetHorizontalScrollMode(FrameworkElement instance, string value)
        {
            if (value != null && typeof(ScrollMode).GetConstants().ContainsKey(value))
            {
                instance.SetValue(ScrollViewerProperties.HorizontalScrollMode, value);
            }
        }

        public static void SetVerticalScrollBarVisibility(FrameworkElement instance, string value)
        {
            if (value != null && typeof(ScrollBarVisibility).GetConstants().ContainsKey(value))
            {
                instance.SetValue(ScrollViewerProperties.VerticalScrollBarVisibility, value);
            }
        }

        public static void SetVerticalScrollMode(FrameworkElement instance, string value)
        {
            if (value != null && typeof(ScrollMode).GetConstants().ContainsKey(value))
            {
                instance.SetValue(ScrollViewerProperties.VerticalScrollMode, value);
            }
        }

        public override object Clone()
        {
            var element = new ScrollViewer();
            element.CopyFrom(this);
            return element;
        }

        public string HorizontalScrollBarVisibility
        {
            get
            {
                return (string)GetValue(ScrollViewerProperties.HorizontalScrollBarVisibility);
            }
            set
            {
                SetValue(ScrollViewerProperties.HorizontalScrollBarVisibility, value);
            }
        }

        public string HorizontalScrollMode
        {
            get
            {
                return (string)GetValue(ScrollViewerProperties.HorizontalScrollMode);
            }
            set
            {
                SetValue(ScrollViewerProperties.HorizontalScrollMode, value);
            }
        }

        public string VerticalScrollBarVisibility
        {
            get
            {
                return (string)GetValue(ScrollViewerProperties.VerticalScrollBarVisibility);
            }
            set
            {
                SetValue(ScrollViewerProperties.VerticalScrollBarVisibility, value);
            }
        }

        public string VerticalScrollMode
        {
            get
            {
                return (string)GetValue(ScrollViewerProperties.VerticalScrollMode);
            }
            set
            {
                SetValue(nameof(ScrollViewerProperties.VerticalScrollMode), value);
            }
        }
    }
}
