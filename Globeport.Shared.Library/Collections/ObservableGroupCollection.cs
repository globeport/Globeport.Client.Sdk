using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Globeport.Shared.Library.Components;

namespace Globeport.Shared.Library.Collections
{
    public class ObservableGroupCollection<TItem> : Observable where TItem : class
    {
        public ObservableList<TItem> Collection { get; private set; }
        public Func<TItem, IGroupDescriptor> GroupSelector { get; set; }

        public ObservableGroupCollection(ObservableList<TItem> collection, Func<TItem, IGroupDescriptor> groupSelector)
        {
            Groups = new ObservableList<BindableGroup<TItem>>((i, j) => i.Descriptor.Key.CompareTo(j.Descriptor.Key));
            Collection = collection;
            GroupSelector = groupSelector;
            Collection.CollectionChanged += Collection_CollectionChanged;
        }

        private void Collection_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Reset)
            {
                Groups.Clear();
                foreach (var item in Collection.GroupBy(i => GroupSelector(i)))
                {
                    Groups.Add(new BindableGroup<TItem>(item.ToList(), item.Key, Collection.Comparer, Collection.IsReverseOrder));
                }
            }
            else
            {
                if (e.NewItems != null)
                {
                    foreach (TItem item in e.NewItems)
                    {
                        var group = AddGroup(GroupSelector(item));
                        group.Add(item);
                    }
                }
                if (e.OldItems != null)
                {
                    foreach (TItem item in e.OldItems)
                    {
                        var groupDescriptor = GroupSelector(item);
                        var group = Groups.FirstOrDefault(i => i.Descriptor.Key.CompareTo(groupDescriptor.Key) == 0);
                        if (group != null)
                        {
                            group.Remove(item);
                            if (group.Count == 0) Groups.Remove(group);
                        }
                    }
                }
            }
        }

        public BindableGroup<TItem> AddGroup(IGroupDescriptor groupDescriptor)
        {
            var group = Groups.FirstOrDefault(i => i.Descriptor.Key.CompareTo(groupDescriptor.Key) == 0);
            if (group == null)
            {
                group = new BindableGroup<TItem>(groupDescriptor, Collection.Comparer, Collection.IsReverseOrder);
                Groups.Add(group);
            }
            return group;
        }

        ObservableList<BindableGroup<TItem>> groups;
        public ObservableList<BindableGroup<TItem>> Groups
        {
            get
            {
                return groups;
            }
            set
            {
                if (groups != value)
                {
                    groups = value;
                    OnPropertyChanged(nameof(Groups));
                }
            }
        }
    }

    public class BindableGroup<TItem> : ObservableList<TItem>, IBindableGroup where TItem : class
    {
        public IGroupDescriptor Descriptor { get; set; }

        public BindableGroup(IGroupDescriptor descriptor, IComparer<TItem> comparer, bool isReverseOrder = false) : base(comparer, isReverseOrder)
        {
            Descriptor = descriptor;
        }

        public BindableGroup(IEnumerable<TItem> items, IGroupDescriptor descriptor, IComparer<TItem> comparer, bool isReverseOrder = false) : base(comparer, isReverseOrder)
        {
            Descriptor = descriptor;
            AddRange(items);
        }
    }

    public interface IBindableGroup
    {
        IGroupDescriptor Descriptor { get; }
    }

    public interface IGroupDescriptor : INotifyPropertyChanged
    {
        IComparable Key { get; set; }
        string Image { get; set; }
        string Label { get; set; }
        string Foreground { get; set; }
        string Background { get; set; }
        bool IsVisible { get; set; }
        Command LeftTap { get; set; }
    }
}
