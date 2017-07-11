using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Runtime.Serialization;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.IO;

using Portable.Xaml;
using Portable.Xaml.Markup;

using Globeport.Shared.Library.Interfaces;
using Globeport.Shared.Library.Extensions;

[assembly: XmlnsDefinition("", "Globeport.Shared.Library.Xaml", AssemblyName = "Globeport.Shared.Library")]

namespace Globeport.Shared.Library.Xaml
{ 
    public class FrameworkElement : UIElement, IFrameworkElement
    {
        public override string Type => nameof(FrameworkElement);
        public string Loaded { get; set; }
        public string SizeChanged { get; set; }

        public FrameworkElement()
        {
            actualWidth = double.NaN;
            actualHeight = double.NaN;
            width = double.NaN;
            height = double.NaN;
            maxWidth = double.PositiveInfinity;
            maxHeight = double.PositiveInfinity;
        }

        public override object Clone()
        {
            var element = new FrameworkElement();
            element.CopyFrom(this);
            return element;
        }

        public override void CopyFrom(DependencyObject element)
        {
            var source = (FrameworkElement)element;
            Loaded = source.Loaded;
            SizeChanged = source.SizeChanged;
            MinWidth = source.MinWidth;
            MinHeight = source.MinHeight;
            MaxWidth = source.MaxWidth;
            MaxHeight = source.MaxHeight;
            Width = source.Width;
            Height = source.Height;
            ActualWidth = source.ActualWidth;
            ActualHeight = source.ActualHeight;
            Margin = source.Margin;
            HorizontalAlignment = source.HorizontalAlignment;
            VerticalAlignment = source.VerticalAlignment;
            RequestedTheme = source.RequestedTheme;
            base.CopyFrom(source);
        }

        public static FrameworkElement ParseXaml(string xaml)
        {
            if (xaml.IsNullOrEmpty()) return null;
            var textReader = new StringReader(xaml.Replace("PropertyChanged=", "PropertyUpdated="));
            var xamlReader = new XamlXmlReader(textReader, new XamlSchemaContext(new[] { typeof(FrameworkElement).GetTypeInfo().Assembly }));
            var settings = new XamlObjectWriterSettings();
            settings.XamlSetValueHandler += (s, e) =>
            {
                var binding = e.Value as Binding;
                if (binding != null)
                {
                    var element = s as DependencyObject;
                    if (element.Bindings == null) element.Bindings = new Bindings();
                    binding.PropertyName = e.Member.Name;
                    element.Bindings.Add(e.Member.Name, binding);
                    e.Handled = true;
                }
            };
            var xamlWriter = new XamlObjectWriter(xamlReader.SchemaContext, settings);

            if (xamlReader.NodeType == XamlNodeType.None)
                xamlReader.Read();

            while (!xamlReader.IsEof)
            {
                xamlWriter.WriteNode(xamlReader);
                xamlReader.Read();
            }

            xamlWriter.Close();

            return (FrameworkElement)xamlWriter.Result;
        }

        double minWidth;
        public double MinWidth
        {
            get
            {
                return minWidth;
            }
            set
            {
                if (minWidth != value && value >= 0)
                {
                    minWidth = value;
                    OnPropertyChanged(nameof(MinWidth));
                }
            }
        }

        double minHeight;
        public double MinHeight
        {
            get
            {
                return minHeight;
            }
            set
            {
                if (minHeight != value && value >= 0)
                {
                    minHeight = value;
                    OnPropertyChanged(nameof(MinHeight));
                }
            }
        }

        double maxWidth;
        public double MaxWidth
        {
            get
            {
                return maxWidth;
            }
            set
            {
                if (maxWidth != value && value >= 0)
                {
                    maxWidth = value;
                    OnPropertyChanged(nameof(MaxWidth));
                }
            }
        }

        double maxHeight;
        public double MaxHeight
        {
            get
            {
                return maxHeight;
            }
            set
            {
                if (maxHeight != value && value >= 0)
                {
                    maxHeight = value;
                    OnPropertyChanged(nameof(MaxHeight));
                }
            }
        }

        double width;
        public double Width
        {
            get
            {
                return width;
            }
            set
            {
                if (width != value && value >= 0)
                {
                    width = value;
                    OnPropertyChanged(nameof(Width));
                }
            }
        }

        double height;
        public double Height
        {
            get
            {
                return height;
            }
            set
            {
                if (height != value && value >= 0)
                {
                    height = value;
                    OnPropertyChanged(nameof(Height));
                }
            }
        }

        double actualWidth;
        public double ActualWidth
        {
            get
            {
                return actualWidth;
            }
            set
            {
                if (actualWidth != value)
                {
                    actualWidth = value;
                    OnPropertyChanged(nameof(ActualWidth));
                }
            }
        }

        double actualHeight;
        public double ActualHeight
        {
            get
            {
                return actualHeight;
            }
            set
            {
                if (actualHeight != value)
                {
                    actualHeight = value;
                    OnPropertyChanged(nameof(ActualHeight));
                }
            }
        }

        string margin;
        public string Margin
        {
            get
            {
                return margin;
            }
            set
            {
                if (margin != value)
                {
                    margin = value;
                    OnPropertyChanged(nameof(Margin));
                }
            }
        }

        string horizontalAlignment;
        public string HorizontalAlignment
        {
            get
            {
                return horizontalAlignment;
            }
            set
            {
                if (value != null && horizontalAlignment != value && typeof(HorizontalAlignment).GetConstants().ContainsKey(value))
                {
                    horizontalAlignment = value;
                    OnPropertyChanged(nameof(HorizontalAlignment));
                }
            }
        }

        string verticalAlignment;
        public string VerticalAlignment
        {
            get
            {
                return verticalAlignment;
            }
            set
            {
                if (value != null && verticalAlignment != value && typeof(VerticalAlignment).GetConstants().ContainsKey(value))
                {
                    verticalAlignment = value;
                    OnPropertyChanged(nameof(VerticalAlignment));
                }
            }
        }

        string requestedTheme;
        public string RequestedTheme
        {
            get
            {
                return requestedTheme;
            }
            set
            {
                if (value != null && requestedTheme != value && typeof(ElementTheme).GetConstants().ContainsKey(value))
                {
                    requestedTheme = value;
                    OnPropertyChanged(nameof(RequestedTheme));
                }
            }
        }
    }
}
