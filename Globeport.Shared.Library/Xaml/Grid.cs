using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Globeport.Shared.Library.Interfaces;

namespace Globeport.Shared.Library.Xaml
{
    public class Grid : Panel, IGrid
    {
        public override string Type => nameof(Grid);
        public ColumnDefinitions ColumnDefinitions { get; set; } = new ColumnDefinitions();
        public RowDefinitions RowDefinitions { get; set; } = new RowDefinitions();

        public Grid()
        {
        }

        public override List<DependencyObject> GetElements()
        {
            var elements = base.GetElements();
            foreach (var col in ColumnDefinitions)
            {
                elements.AddRange(col.GetElements());
            }
            foreach (var row in RowDefinitions)
            {
                elements.AddRange(row.GetElements());
            }
            return elements;
        }

        public static void SetColumn(FrameworkElement instance, int value)
        {
            if (value>=0) instance.SetValue(GridProperties.Column, value);
        }

        public static void SetRow(FrameworkElement instance, int value)
        {
            if (value >= 0) instance.SetValue(GridProperties.Row, value);
        }

        public static void SetRowSpan(FrameworkElement instance, int value)
        {
            if (value >= 0) instance.SetValue(GridProperties.RowSpan, value);
        }

        public static void SetColumnSpan(FrameworkElement instance, int value)
        {
            if (value >= 0) instance.SetValue(GridProperties.ColumnSpan, value);
        }

        public override object Clone()
        {
            var element = new Grid();
            element.CopyFrom(this);
            return element;
        }

        public override void CopyFrom(DependencyObject element)
        {
            var source = (Grid)element;
            BorderThickness = source.BorderThickness;
            BorderBrush = source.BorderBrush;
            CornerRadius = source.CornerRadius;
            Padding = source.Padding;
            RowDefinitions = new RowDefinitions(source.RowDefinitions.Select(i => (RowDefinition)i.Clone()));
            ColumnDefinitions = new ColumnDefinitions(source.ColumnDefinitions.Select(i => (ColumnDefinition)i.Clone()));
            base.CopyFrom(source);
        }

        string borderThickness;
        public string BorderThickness
        {
            get
            {
                return borderThickness;
            }
            set
            {
                if (value != borderThickness)
                {
                    borderThickness = value;
                    OnPropertyChanged(nameof(BorderThickness));
                }
            }
        }

        string borderBrush;
        public string BorderBrush
        {
            get
            {
                return borderBrush;
            }
            set
            {
                if (value != borderBrush)
                {
                    borderBrush = value;
                    OnPropertyChanged(nameof(BorderBrush));
                }
            }
        }

        string cornerRadius;
        public string CornerRadius
        {
            get
            {
                return cornerRadius;
            }
            set
            {
                if (value != cornerRadius)
                {
                    cornerRadius = value;
                    OnPropertyChanged(nameof(CornerRadius));
                }
            }
        }

        string padding;
        public string Padding
        {
            get
            {
                return padding;
            }
            set
            {
                if (padding != value)
                {
                    padding = value;
                    OnPropertyChanged(nameof(Padding));
                }
            }
        }
    }
}
