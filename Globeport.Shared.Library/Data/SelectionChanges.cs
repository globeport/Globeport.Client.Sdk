using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Globeport.Shared.Library.Collections;

namespace Globeport.Shared.Library.Data
{
    public class SelectionChanges<T> 
    {
        public event EventHandler Changed;
        public ObservableDictionary<string, T> Added { get; } = new ObservableDictionary<string, T>();
        public ObservableDictionary<string, T> Removed { get; } = new ObservableDictionary<string, T>();
        public ObservableDictionary<string, T> Original { get; } = new ObservableDictionary<string, T>();

        public SelectionChanges()
        {
            Added.CollectionChanged += Added_CollectionChanged;
            Removed.CollectionChanged += Removed_CollectionChanged;
        }

        public SelectionChanges(ObservableDictionary<string, T> original, ObservableDictionary<string, T> added, ObservableDictionary<string, T> removed)
        {
            Original = original;
            Added = added;
            Removed = removed;
            Added.CollectionChanged += Added_CollectionChanged;
            Removed.CollectionChanged += Removed_CollectionChanged;
        }

        protected virtual void OnChanged()
        {
            Changed?.Invoke(this, EventArgs.Empty);
        }

        public bool HasChanges()
        {
            return Added.Count > 0 || Removed.Count > 0;
        }

        public void Add(string key, T value)
        {
            Added.Add(key, value);
            OnChanged();
        }

        public void Remove(string key)
        {
            Removed.Remove(key);
            OnChanged();
        }

        public void Reset()
        {
            Added.Clear();
            Removed.Clear();
            Original.Clear();
        }

        public SelectionChanges<T> Clone()
        {
            return new SelectionChanges<T>(new ObservableDictionary<string, T>(Original), new ObservableDictionary<string, T>(Added), new ObservableDictionary<string, T>(Removed));
        }

        private void Removed_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        { 
            OnChanged();
        }

        private void Added_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            OnChanged();
        }
    }
}
