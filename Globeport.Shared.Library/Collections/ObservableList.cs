using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;

using Globeport.Shared.Library.Components;

namespace Globeport.Shared.Library.Collections
{
    public class ObservableList<T> : ObservableCollection<T> where T : class
    {
        public event EventHandler<ValueChangingEventArgs<int>> CurrentIndexChanging;
        public event EventHandler<ValueChangedEventArgs<int>> CurrentIndexChanged;

        public bool IsNotifying { get; set; } = true;
        public bool IsReverseOrder { get; set; }
        public bool IsOrdered { get; private set; }
        public IComparer<T> Comparer { get; private set; }

        const int SimpleAlgorithmThreshold = 10;

        readonly object itemsLock = new object();

        public ObservableList()
        { 
        }

        public ObservableList(IEnumerable<T> collection)
            : base(collection)
        {
        }

        public ObservableList(IComparer<T> comparer, bool isReverseOrder = false)
            : this()
        {
            Comparer = comparer;
            IsReverseOrder = isReverseOrder;
            IsOrdered = true;
        }

        public ObservableList(Comparison<T> comparison, bool isReverseOrder = false)
            : this()
        {
            Comparer = new ComparisonComparer<T>(comparison);
            IsReverseOrder = isReverseOrder;
            IsOrdered = true;
        }

        protected virtual void NotifyPropertyChanged(string propertyName)
        {
            if (IsNotifying) OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
        }

        public void Refresh()
        {
            OnPropertyChanged(new PropertyChangedEventArgs("Count"));
            OnPropertyChanged(new PropertyChangedEventArgs("Item[]"));
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }

        protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            if (IsNotifying)
            {                
                Dispatcher.Invoke(() =>
                {
                    lock (itemsLock)
                    {
                        try
                        {
                            base.OnCollectionChanged(e);
                        }
                        catch (Exception ex)
                        {
                            
                        }
                    }
                });
            }
        }

        protected override void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            if (IsNotifying)
            {
                Dispatcher.Invoke(() => base.OnPropertyChanged(e));
            }
        }

        protected override void InsertItem(int index, T item)
        {
            try
            {
                lock (itemsLock)
                {
                    if (IsOrdered) index = GetInsertIndex(item);
                    base.InsertItem(index, item);
                }
            }
            catch (Exception e)
            {
                System.Diagnostics.Debugger.Break();
            }
        }

        protected override void SetItem(int index, T item)
        {
            lock (itemsLock)
            {
                base.SetItem(index, item);
            }
        }

        protected override void RemoveItem(int index)
        {
            lock (itemsLock)
            {
                base.RemoveItem(index);
            }
        }

        public new void Remove(T item)
        {
            lock (itemsLock)
            {
                base.Remove(item);
            }
        }

        public virtual void AddRange(IEnumerable<T> items)
        {
            lock (itemsLock)
            {
                var previousNotificationStatus = IsNotifying;

                IsNotifying = false;

                var index = Count;

                foreach (var item in items)
                {
                    if (IsOrdered) index = GetInsertIndex(item);
                    base.InsertItem(index, item);
                    index++;
                }

                IsNotifying = previousNotificationStatus;

                if (previousNotificationStatus) Refresh();
            }
        }

        public virtual void RemoveAll(IEnumerable<T> items)
        {
            lock (itemsLock)
            {
                var previousNotificationStatus = IsNotifying;

                IsNotifying = false;

                foreach (var item in items)
                {
                    var index = IndexOf(item);
                    if (index >= 0)
                    {
                        base.RemoveItem(index);
                    }
                }

                IsNotifying = previousNotificationStatus;

                if (previousNotificationStatus) Refresh();
            }
        }

        protected override void ClearItems()
        {
            lock (itemsLock)
            {
                base.ClearItems();
            }
        }

        public NotificationSuppressor<T> SuppressNotifications()
        {
            return new NotificationSuppressor<T>(this);
        }

        protected virtual int Compare(T x, T y)
        {
            return Comparer.Compare(x, y);
        }

        int ReverseComparison(int comparison)
        {
            return IsReverseOrder ? -comparison : comparison;
        }

        int GetInsertIndex(T item)
        {
            if (Count == 0) return 0;
            return Count <= SimpleAlgorithmThreshold ? GetInsertIndexSimple(item) : GetInsertIndexComplex(item);
        }

        int GetInsertIndexSimple(T item)
        {
            for (int i = 0; i < Items.Count; i++)
            {
                T existingItem = Items[i];
                int comparison = ReverseComparison(Compare(existingItem, item));
                if (comparison == 0) return -1;
                if (comparison > 0) return i;
            }
            return Count;
        }

        int GetInsertIndexComplex(T item)
        {
            int minIndex = 0, maxIndex = Count - 1;
            while (minIndex <= maxIndex)
            {
                int pivotIndex = (maxIndex + minIndex) / 2;
                int comparison = ReverseComparison(Compare(item, Items[pivotIndex]));
                if (comparison == 0) return -1;
                if (comparison < 0)
                    maxIndex = pivotIndex - 1;
                else
                    minIndex = pivotIndex + 1;
            }
            return minIndex;
        }

        protected override void MoveItem(int oldIndex, int newIndex)
        {
            if (IsOrdered)
            {
                throw new InvalidOperationException("Cannot move items in an ordered collection");
            }
            base.MoveItem(oldIndex, newIndex);
        }

        protected virtual void OnCurrentIndexChanging(ValueChangingEventArgs<int> e)
        {
            CurrentIndexChanging?.Invoke(this, e);
        }

        protected virtual void OnCurrentIndexChanged(ValueChangedEventArgs<int> e)
        {
            CurrentIndexChanged?.Invoke(this, e);
        }


        T currentItem;
        public T CurrentItem
        {
            get
            {
                return currentItem;
            }
            set
            {
                if (currentItem != value)
                {
                    CurrentIndex = IndexOf(value);
                }
            }
        }

        int currentIndex = -1;
        public int CurrentIndex
        {
            get
            {
                return currentIndex;
            }
            set
            {
                if (currentIndex != value)
                {
                    var previousIndex = currentIndex;
                    OnCurrentIndexChanging(new ValueChangingEventArgs<int>(previousIndex, value));
                    currentIndex = value;
                    currentItem = Count > 0 && currentIndex < Count && currentIndex >= 0 ? this[currentIndex] : null;
                    OnCurrentIndexChanged(new ValueChangedEventArgs<int>(previousIndex, value));
                    NotifyPropertyChanged(nameof(CurrentIndex));
                    NotifyPropertyChanged(nameof(CurrentItem));
                }
            }
        }

        class ComparableComparer<TItem> : IComparer<TItem>
        {
            int IComparer<TItem>.Compare(TItem x, TItem y)
            {
                return ((IComparable<TItem>)x).CompareTo(y);
            }
        }

        class ComparisonComparer<TItem> : IComparer<TItem>
        {
            private readonly Comparison<TItem> comparison;

            internal ComparisonComparer(Comparison<TItem> comparison)
            {
                this.comparison = comparison;
            }

            int IComparer<TItem>.Compare(TItem x, TItem y)
            {
                return comparison(x, y);
            }
        }   
    }

    public class NotificationSuppressor<T> : IDisposable where T : class
    {
        ObservableList<T> collection;
        bool previousNotificationStatus;

        public NotificationSuppressor(ObservableList<T> collection)
        {
            this.collection = collection;
            previousNotificationStatus = collection.IsNotifying;
            collection.IsNotifying = false;
        }

        public void Dispose()
        {
            collection.IsNotifying = previousNotificationStatus;
            if (previousNotificationStatus) collection.Refresh();
        }
    }
}
