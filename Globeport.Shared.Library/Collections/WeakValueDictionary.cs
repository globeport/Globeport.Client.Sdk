﻿// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
//-----------------------------------------------------------------------
// </copyright>
// <summary>Dictionary that does not prevent values from being garbage collected.</summary>
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace Globeport.Shared.Library.Collections
{
    /// <summary>
    /// Dictionary that does not prevent values from being garbage collected.
    /// </summary>
    /// <typeparam name="K">Type of key</typeparam>
    /// <typeparam name="V">Type of value, without the WeakReference wrapper.</typeparam>
    public class WeakValueDictionary<K, V>
        where V : class
    {
        /// <summary>
        /// The dictionary used internally to store the keys and values.
        /// </summary>
        [DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
        private Dictionary<K, WeakReference<V>> _dictionary;

        /// <summary>
        /// Improvised capacity. See comment in item setter.
        /// </summary>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private int _capacity = 10;

        /// <summary>
        /// Constructor for a collection using the default key comparer
        /// </summary>
        public WeakValueDictionary()
            : this(null)
        {
        }

        /// <summary>
        /// Constructor taking a specified comparer for the keys
        /// </summary>
        public WeakValueDictionary(IEqualityComparer<K> keyComparer)
        {
            _dictionary = new Dictionary<K, WeakReference<V>>(_capacity, keyComparer);
        }

        /// <summary>
        /// Count of entries.
        /// Some entries may represent keys or values that have already been garbage collected.
        /// To clean these out call <see cref="Scavenge"/>.
        /// </summary>
        public int Count
        {
            get { return _dictionary.Count; }
        }

        /// <summary>
        /// Return keys
        /// </summary>
        public IEnumerable<K> Keys
        {
            get
            {
                List<K> keys = new List<K>();

                foreach (KeyValuePair<K, WeakReference<V>> pair in _dictionary)
                {
                    V target;
                    if (pair.Value != null && pair.Value.TryGetTarget(out target) && target != null)
                    {
                        keys.Add(pair.Key);
                    }
                }

                return keys;
            }
        }

        /// <summary>
        /// Obtains the value for a given key.
        /// </summary>
        /// <remarks>
        /// If we find the entry but its value's target is null, we take the opportunity
        /// to remove the entry, as if the GC had done it.
        /// </remarks>
        public V this[K key]
        {
            get
            {
                WeakReference<V> wrappedValue = _dictionary[key];

                if (wrappedValue == null)
                {
                    // We use this to represent an actual value
                    // that is null, rather than a collected non-null value
                    return null;
                }

                V value;

                if (!wrappedValue.TryGetTarget(out value))
                {
                    _dictionary.Remove(key);

                    // Trigger KeyNotFoundException
                    wrappedValue = _dictionary[key];

                    throw new KeyNotFoundException();
                }

                return value;
            }

            set
            {
                // Make some attempt to prevent dictionary growing forever with
                // entries whose underlying key or value has already been collected.
                // We do not have access to the dictionary's true capacity or growth
                // method, so we improvise with our own.
                // So attempt to make room for the upcoming add before we do it.
                if (_dictionary.Count == _capacity)
                {
                    Scavenge();

                    // If that didn't do anything, raise the capacity at which 
                    // we next scavenge. Note that we never shrink, but neither
                    // does the underlying dictionary.
                    if (_dictionary.Count == _capacity)
                    {
                        _capacity = _dictionary.Count * 2;
                    }
                }

                // Use a null value to represent real null, as opposed to a collected real value
                WeakReference<V> wrappedValue = (value == null) ? null : new WeakReference<V>(value);

                _dictionary[key] = wrappedValue;
            }
        }

        /// <summary>
        /// Whether there is a key present with the specified key
        /// </summary>
        /// <remarks>
        /// As usual, don't just call Contained as the wrapped value may be null.
        /// </remarks>
        public bool Contains(K key)
        {
            V value;
            bool contained = TryGetValue(key, out value);
            return contained;
        }

        /// <summary>
        /// Attempts to get the value for the provided key.
        /// Returns true if the key is found, otherwise false.
        /// </summary>
        /// <remarks>
        /// If we find the entry but its value's target is null, we take the opportunity
        /// to remove the entry, as if the GC had done it.
        /// </remarks>
        public bool TryGetValue(K key, out V value)
        {
            WeakReference<V> wrappedValue;
            bool result = _dictionary.TryGetValue(key, out wrappedValue);

            if (!result)
            {
                value = null;
                return false;
            }

            if (wrappedValue == null)
            {
                // We use this to represent an actual value
                // that is null, rather than a collected non-null value
                value = null;
                return true;
            }

            if (!wrappedValue.TryGetTarget(out value))
            {
                _dictionary.Remove(key);
                return false;
            }

            return result;
        }

        /// <summary>
        /// Removes an entry with the specified key.
        /// Returns true if found, false otherwise.
        /// </summary>
        public bool Remove(K key)
        {
            return _dictionary.Remove(key);
        }

        /// <summary>
        /// Remove any entries from the dictionary that represent keys
        /// that have been garbage collected.
        /// </summary>
        /// <returns>The number of entries removed.</returns>
        public int Scavenge()
        {
            List<K> remove = null;

            foreach (KeyValuePair<K, WeakReference<V>> entry in _dictionary)
            {
                if (entry.Value == null)
                {
                    // We use this to represent an actual value
                    // that is null, rather than a collected non-null value
                    continue;
                }

                V value;

                if (!entry.Value.TryGetTarget(out value))
                {
                    remove = remove ?? new List<K>();
                    remove.Add(entry.Key);
                }
            }

            if (remove != null)
            {
                foreach (K entry in remove)
                {
                    _dictionary.Remove(entry);
                }

                return remove.Count;
            }

            return 0;
        }

        /// <summary>
        /// Empty the collection
        /// </summary>
        public void Clear()
        {
            _dictionary.Clear();
        }
    }
}