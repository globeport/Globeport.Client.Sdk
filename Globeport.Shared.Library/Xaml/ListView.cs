using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using System.Collections.Specialized;

using Globeport.Shared.Library.Interfaces;
using Globeport.Shared.Library.Extensions;

namespace Globeport.Shared.Library.Xaml
{
    public class ListView : Selector, IListView
    {
        public override string Type => nameof(ListView);

        public string ContainerContentChanging { get; set; }

        public bool IsSelectionChanging { get; set; }

        public ListView()
        {
            SelectionMode = ListViewSelectionMode.None;
        }

        public override void Load(IXamlHost host)
        {
            selectedItems = new ItemsCollection();
            selectedItems.CollectionChanged += SelectedItems_CollectionChanged;
            base.Load(host);
        }

        public override List<DependencyObject> GetElements()
        {
            var elements = base.GetElements();
            if (Header is FrameworkElement) elements.AddRange(((FrameworkElement)Header).GetElements());
            return elements;
        }

        public override object Clone()
        {
            var element = new ListView();
            element.CopyFrom(this);
            return element;
        }

        public override void CopyFrom(DependencyObject element)
        {
            var source = (ListView)element;
            if (source.Header is FrameworkElement)
            {
                Header = ((FrameworkElement)source.Header).Clone();
            }
            else
            {
                Header = source.Header;
            }
            ContainerContentChanging = source.ContainerContentChanging;
            SelectionMode = source.SelectionMode;
            IsItemClickEnabled = source.IsItemClickEnabled;
            base.CopyFrom(source);
        }

        private void SelectedItems_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            IsSelectionChanging = true;
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    if (e.NewItems != null && e.NewItems.Count > 0)
                    {
                        Host.CallFunction($"Hosts[{Host.Id}].Elements[{Id}].Select", new object[] { this.Items.IndexOf(e.NewItems[0]) });
                    }
                    break;
                case NotifyCollectionChangedAction.Remove:
                    if (e.OldItems != null && e.OldItems.Count > 0)
                    {
                        Host.CallFunction($"Hosts[{Host.Id}].Elements[{Id}].Unselect", new object[] { e.OldStartingIndex });
                    }
                    break;
                case NotifyCollectionChangedAction.Reset:
                    break;
            }
            IsSelectionChanging = false;
        }

        object header;
        public object Header
        {
            get
            {
                return header;
            }
            set
            {
                if (value != header && (value == null || value is FrameworkElement || value is string))
                {
                    header = value;
                    OnPropertyChanged(nameof(Header));
                }
            }
        }

        string selectionMode;
        public string SelectionMode
        {
            get
            {
                return selectionMode;
            }
            set
            {
                if (value != null && value != selectionMode && typeof(ListViewSelectionMode).GetConstants().ContainsKey(value))
                {
                    selectionMode = value;
                    OnPropertyChanged(nameof(SelectionMode));
                }
            }
        }

        ItemsCollection selectedItems;
        public object SelectedItems
        {
            get
            {
                return selectedItems;
            }
            set
            {
                if (value != selectedItems && value is IEnumerable<object>)
                {
                    selectedItems.Clear();
                    selectedItems.AddRange((IEnumerable<object>)value);
                }
            }
        }

        bool isItemClickEnabled;
        public bool IsItemClickEnabled
        {
            get
            {
                return isItemClickEnabled;
            }
            set
            {
                if (isItemClickEnabled != value)
                {
                    isItemClickEnabled = value;
                    OnPropertyChanged(nameof(IsItemClickEnabled));
                }
            }
        }
    }
}
