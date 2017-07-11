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
    public class ItemsStackPanel : Panel, IItemsStackPanel
    {
        public override string Type => nameof(ItemsStackPanel);

        public ItemsStackPanel()
        {
        }

        public override object Clone()
        {
            var element = new ItemsStackPanel();
            element.CopyFrom(this);
            return element;
        }

        public override void CopyFrom(DependencyObject element)
        {
            var source = (ItemsStackPanel)element;
            Orientation = source.Orientation;
            base.CopyFrom(source);
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
