﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

using Globeport.Shared.Library.Interfaces;
using Globeport.Shared.Library.Extensions;

namespace Globeport.Shared.Library.Xaml
{
    public class ImageBrush : Brush, IImage
    {
        public override string Type => nameof(ImageBrush);

        public ImageBrush()
        {
        }

        public override object Clone()
        {
            var element = new ImageBrush();
            element.CopyFrom(this);
            return element;
        }

        public override void CopyFrom(DependencyObject element)
        {
            var source = (ImageBrush)element;
            Source = source.Source;
            Size = source.Size;
            Stretch = source.Stretch;
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
                if (source != value)
                {
                    source = value;
                    OnPropertyChanged(nameof(Source));
                }
            }
        }

        int size;
        public int Size
        {
            get
            {
                return size;
            }
            set
            {
                if (size != value)
                {
                    size = value;
                    OnPropertyChanged(nameof(Size));
                }
            }
        }

        string stretch;
        public string Stretch
        {
            get
            {
                return stretch;
            }
            set
            {
                if (value != null && value != stretch && typeof(Stretch).GetConstants().ContainsKey(value))
                {
                    stretch = value;
                    OnPropertyChanged(nameof(Stretch));
                }
            }
        }
    }
}
