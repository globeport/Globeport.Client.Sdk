using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

using Portable.Xaml.Markup;

using Globeport.Shared.Library.Interfaces;

namespace Globeport.Shared.Library.Xaml
{
    [ContentProperty("Items")]
    public class ItemsControl : Control, IItemsControl
    {
        public override string Type => nameof(ItemsControl);

        public string ItemsChanged { get; set; }

        public ItemsControl()
        {
        }

        public override void Load(IXamlHost host)
        {
            items = new ItemsCollection();
            itemsSource = new ItemsCollection();
            base.Load(host);
        }

        public override List<DependencyObject> GetElements()
        {
            var elements = base.GetElements();
            if (ItemBackground is ImageBrush)
            {
                elements.AddRange(((ImageBrush)ItemBackground).GetElements());
            }
            return elements;
        }

        public virtual void InsertItem(int index, object item)
        {
            if (ItemTemplate != null)
            {
                var element = (FrameworkElement)ItemTemplate.Template.Clone();
                Host.LoadElement(element);
                Items.Insert(index, element);
            }
            else
            {
                Items.Insert(index, item);
            }
        }

        public virtual void RemoveItem(int index)
        {
            if (ItemTemplate != null)
            {
                var element = (DependencyObject)Items[index];
                if (element != null)
                {
                    Host.UnloadElement(element);
                }
            }
            Items.RemoveAt(index);
        }

        public override object Clone()
        {
            var element = new ItemsControl();
            element.CopyFrom(this);
            return element;
        }

        public override void CopyFrom(DependencyObject element)
        {
            var source = (ItemsControl)element;
            ItemTemplate = (DataTemplate)source.ItemTemplate?.Clone();
            ItemsPanel = (ItemsPanelTemplate)source.ItemsPanel?.Clone();
            ItemsChanged = source.ItemsChanged;
            ItemMargin = source.ItemMargin;
            ItemPadding = source.ItemPadding;
            if (source.ItemBackground is ImageBrush)
            {
                ItemBackground = ((ImageBrush)source.ItemBackground).Clone();
            }
            else
            {
                ItemBackground = source.ItemBackground;
            }
            base.CopyFrom(source);
        }

        ItemsCollection itemsSource;
        public object ItemsSource
        {
            get
            {
                return itemsSource;
            }
            set
            {
                if (itemsSource != value && value is IEnumerable<object>)
                {
                    foreach (var item in itemsSource) RemoveItem(0);
                    itemsSource = new ItemsCollection((IEnumerable<object>)value);
                    Items = new ItemsCollection();
                    var index = 0;
                    foreach (var item in itemsSource) InsertItem(index++, item);
                    OnPropertyChanged(nameof(ItemsSource));
                }
            }
        }

        ItemsCollection items;
        public ItemsCollection Items
        {
            get
            {
                return items;
            }
            set
            {
                if (items != value)
                {
                    items = value;
                    OnPropertyChanged(nameof(Items));
                }
            }
        }

        DataTemplate itemTemplate;
        public DataTemplate ItemTemplate
        {
            get
            {
                return itemTemplate;
            }
            set
            {
                if (value != itemTemplate)
                {
                    itemTemplate = value;
                    OnPropertyChanged(nameof(ItemTemplate));
                }
            }
        }

        ItemsPanelTemplate itemsPanel;
        public ItemsPanelTemplate ItemsPanel
        {
            get
            {
                return itemsPanel;
            }
            set
            {
                if (value != itemsPanel)
                {
                    itemsPanel = value;
                    OnPropertyChanged(nameof(ItemsPanel));
                }
            }
        }

        string itemMargin;
        public string ItemMargin
        {
            get
            {
                return itemMargin;
            }
            set
            {
                if (value != itemMargin)
                {
                    itemMargin = value;
                    OnPropertyChanged(nameof(ItemMargin));
                }
            }
        }

        string itemPadding;
        public string ItemPadding
        {
            get
            {
                return itemPadding;
            }
            set
            {
                if (value != itemPadding)
                {
                    itemPadding = value;
                    OnPropertyChanged(nameof(ItemPadding));
                }
            }
        }

        object itemBackground;
        public object ItemBackground
        {
            get
            {
                return itemBackground;
            }
            set
            {
                if (value != itemBackground & (value == null || value is ImageBrush || value is string))
                {
                    itemBackground = value;
                    OnPropertyChanged(nameof(ItemBackground));
                }
            }
        }
    }
}
