using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Globeport.Shared.Library.Interfaces;

namespace Globeport.Shared.Library.Xaml
{
    public class MenuFlyoutItem : Control, IMenuFlyoutItem
    {
        public override string Type => nameof(MenuFlyoutItem);

        public MenuFlyoutItem()
        {
        }

        public override object Clone()
        {
            var element = new MenuFlyoutItem();
            element.CopyFrom(this);
            return element;
        }

        public override void CopyFrom(DependencyObject element)
        {
            var source = (MenuFlyoutItem)element;
            Text = source.Text;
            base.CopyFrom(source);
        }

        string text;
        public string Text
        {
            get
            {
                return text;
            }
            set
            {
                if (text != value)
                {
                    text = value;
                    OnPropertyChanged(nameof(Text));
                }
            }
        }
    }
}
