using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

using Globeport.Shared.Library.Interfaces;
using Globeport.Shared.Library.Extensions;

namespace Globeport.Shared.Library.Xaml
{
    public class ItemsWrapGrid : Panel, IItemsWrapGrid
    {
        public override string Type => nameof(ItemsWrapGrid);

        public ItemsWrapGrid()
        {
            maximumRowsOrColumns = -1;
        }

        public override object Clone()
        {
            var element = new ItemsWrapGrid();
            element.CopyFrom(this);
            return element;
        }

        public override void CopyFrom(DependencyObject element)
        {
            var source = (ItemsWrapGrid)element;
            MaximumRowsOrColumns = source.MaximumRowsOrColumns;
            Orientation = source.Orientation;
            base.CopyFrom(source);
        }

        int maximumRowsOrColumns;
        public int MaximumRowsOrColumns
        {
            get
            {
                return maximumRowsOrColumns;
            }
            set
            {
                if (maximumRowsOrColumns != value)
                {
                    maximumRowsOrColumns = value;
                    OnPropertyChanged(nameof(MaximumRowsOrColumns));
                }
            }
        }

        string orientation;
        public string Orientation
        {
            get
            {
                return orientation;
            }
            set
            {
                if (value != null && value != orientation && typeof(Orientation).GetConstants().ContainsKey(value))
                {
                    orientation = value;
                    OnPropertyChanged(nameof(Orientation));
                }
            }
        }
    }
}
