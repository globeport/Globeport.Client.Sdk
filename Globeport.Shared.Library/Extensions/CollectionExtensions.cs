using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Concurrent;

using MoreLinq;

using Globeport.Shared.Library.Extensions;
using Globeport.Shared.Library.ClientModel;

namespace Globeport.Shared.Library.Extensions
{
    public static class CollectionExtensions
    {
        public static int BinarySearch<T>(this List<T> list,
                                         T item,
                                         Func<T, T, int> compare)
        {
            return list.BinarySearch(item, new ComparisonComparer<T>(compare));
        }

        public class ComparisonComparer<T> : IComparer<T>
        {
            private readonly Comparison<T> comparison;

            public ComparisonComparer(Func<T, T, int> compare)
            {
                if (compare == null)
                {
                    throw new ArgumentNullException("comparison");
                }
                comparison = new Comparison<T>(compare);
            }

            public int Compare(T x, T y)
            {
                return comparison(x, y);
            }
        }

        public static void AddOrUpdate<T1, T2>(this IDictionary<T1, T2> dictionary, T1 key, T2 value)
        {
            dictionary[key] = value;
        }

        public static void AddOrUpdate<T1, T2>(this IDictionary<T1, T2> dictionary, IDictionary<T1, T2> items)
        {
            foreach (var item in items)
            {
                dictionary.AddOrUpdate(item.Key, item.Value);
            }
        }

        public static IEnumerable<T> Except<T>(this IEnumerable<T> first, IEnumerable<T> second, Func<T, T, bool> comparer)
        {
            return first.Except(second, new LambdaComparer<T>(comparer));
        }

        public static IEnumerable<T> Intersect<T>(this IEnumerable<T> first, IEnumerable<T> second, Func<T, T, bool> comparer)
        {
            return first.Intersect(second, new LambdaComparer<T>(comparer));
        }

        public static IEnumerable<T> Distinct<T>(this IEnumerable<T> list, Func<T, T, bool> comparer)
        {
            return list.Distinct(new LambdaComparer<T>(comparer));
        }

        public static IEnumerable<T> Union<T>(this IEnumerable<T> first, IEnumerable<T> second, Func<T, T, bool> comparer)
        {
            return first.Union(second, new LambdaComparer<T>(comparer));
        }

        public static IEnumerable<TResult> IfNotEmpty<T, TResult>(this IEnumerable<T> list, Func<IEnumerable<T>, IEnumerable<TResult>> func)
        {
            if (list.IsNullOrEmpty()) return new List<TResult>();
            return func(list);
        }

        public static List<T> AddToList<T>(this T item)
        {
            var list = new List<T>();

            if (item != null) list.Add(item);

            return list;
        }

        public static List<T> AddToList<T>(this T item, List<T> list)
        {
            if (item != null) list.Add(item);

            return list;
        }

        public static IEnumerable<T> EmptyIfNull<T>(this IEnumerable<T> list)
        {
            return list ?? new List<T>();
        }

        public static List<T> EmptyIfNull<T>(this List<T> list)
        {
            return list ?? new List<T>();
        }

        public static Dictionary<TKey, TValue> EmptyIfNull<TKey, TValue>(this Dictionary<TKey, TValue> dictionary)
        {
            return dictionary ?? new Dictionary<TKey, TValue>();
        }

        public static TValue GetValue<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key)
        {
            if (dictionary != null && key != null && dictionary.ContainsKey(key))
            {
                return dictionary[key];
            }
            else
            {
                return default(TValue);
            }
        }

        public static T GetValue<T>(this IDictionary<string, object> dictionary, string key)
        {
            if (dictionary != null && key != null && dictionary.ContainsKey(key))
            {
                return (T)dictionary[key];
            }
            else
            {
                return default(T);
            }
        }

        public static TValue GetOrAdd<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, Func<TValue> valueFunc)
        {
            if (key != null && dictionary.ContainsKey(key))
            {
                return dictionary[key];
            }
            else
            {
                var value = valueFunc();
                dictionary[key] = value;
                return value;
            }
        }

        public static bool TryRemove<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, out TValue value)
        {
            value = dictionary.GetValue(key);
            if (value != null)
            {
                dictionary.Remove(key);
                return true;
            }
            return false;
        }

        public static void RemoveAll<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, Func<KeyValuePair<TKey, TValue>, bool> predicate)
        {
            var items = dictionary.Where(i => predicate(i)).ToList();
            foreach (var item in items)
            {
                dictionary.Remove(item.Key);
            }
        }


        public static bool IsNullOrEmpty<T>(this IEnumerable<T> enumerable)
        {
            return enumerable == null || enumerable.Count() == 0;
        }

        public static IEnumerable<T> Clone<T>(this IEnumerable<T> source) where T : ClientObject, new()
        {
            return source.Select(i => (T)i.Clone()).ToList();
        }

        public static T Pop<T>(this IList<T> list) where T : class
        {
            if (list.IsNullOrEmpty()) return null;
            var item = list[list.Count - 1];
            list.RemoveAt(list.Count - 1);
            return item;
        }

        public static T Peek<T>(this IList<T> list) where T : class
        {
            if (list.IsNullOrEmpty()) return null;
            return list[list.Count - 1];
        }

        public static void Push<T>(this IList<T> list, T item) where T : class
        {
            list.Add(item);
        }

        public static void Enqueue<T>(this IList<T> list, T item) where T : class
        {
            list.Add(item);
        }

        public static T Dequeue<T>(this IList<T> list) where T : class
        {
            if (list.IsNullOrEmpty()) return null;
            var item = list[0];
            list.RemoveAt(0);
            return item;
        }

        public static Dictionary<TKey, TValue> AddEntry<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, TKey key, TValue value)
        {
            dictionary.Add(key, value);
            return dictionary;
        }

        public static IDictionary<TKey, TValue> AddRange<TKey, TValue>(this IDictionary<TKey, TValue> sourceDict, IDictionary<TKey, TValue> concatDict)
        {
            foreach (var item in concatDict)
            {
                sourceDict[item.Key] = item.Value;
            }
            return sourceDict;
        }

        public static void AddRange<T>(this HashSet<T> set, IEnumerable<T> items)
        {
            foreach (var item in items) set.Add(item);
        }

        public static void RemoveRange<T>(this List<T> list, IEnumerable<T> items)
        {
            foreach (var item in items) list.Remove(item);
        }

        public static void RemoveRange<T>(this HashSet<T> set, IEnumerable<T> items)
        {
            foreach (var item in items) set.Remove(item);
        }

        public static void RemoveRange<TKey, TValue>(this IDictionary<TKey,TValue> set, IEnumerable<TKey> keys)
        {
            foreach (var item in keys) set.Remove(item);
        }

        public static HashSet<T> ToHashset<T>(this IEnumerable<T> items)
        {
            return new HashSet<T>(items);
        }

        public static List<T> Append<T>(this List<T> items, T item)
        {
            items.Add(item);
            return items;
        }

        public static IEnumerable<T> Append<T>(this IEnumerable<T> items, params T[] appendItems)
        {
            return items.Concat(appendItems);
        }

        public static List<T> Prepend<T>(this List<T> items, params T[] prependItems)
        {
            var result = prependItems.ToList();
            result.AddRange(items);
            return result;
        }

        public static IEnumerable<T> Prepend<T>(this IEnumerable<T> items, params T[] prependItems)
        {
            return prependItems.Concat(items);
        }

        public static bool TryRemove<TKey, TValue>(this ConcurrentDictionary<TKey, TValue> dictionary, TKey key)
        {
            TValue value;
            return dictionary.TryRemove(key, out value);
        }

        public static void SetValue(this Dictionary<string, string> data, string id, string scopeId, string value)
        {
            if (scopeId != null)
            {
                var scopeData = data.GetValue(scopeId)?.Deserialize<Dictionary<string, string>>() ?? new Dictionary<string, string>();
                scopeData.AddOrUpdate(id, value);
                data[scopeId] = scopeData.Serialize();
            }
            else
            {
                data.AddOrUpdate(id, value);
            }
        }

        public static T[] Combine<T>(this T[] first, T[] second)
        {
            T[] ret = new T[first.Length + second.Length];
            Buffer.BlockCopy(first, 0, ret, 0, first.Length);
            Buffer.BlockCopy(second, 0, ret, first.Length, second.Length);
            return ret;
        }

        public static IEnumerable<T> Distinct<T, TKey>(this IEnumerable<T> list, Func<T, TKey> keySelector)
        {
            return list.DistinctBy(keySelector);
        }

        public static IEnumerable<TResult> LeftJoin<TSource, TInner, TKey, TResult>(this IEnumerable<TSource> source,
                                                 IEnumerable<TInner> inner,
                                                 Func<TSource, TKey> pk,
                                                 Func<TInner, TKey> fk,
                                                 Func<TSource, TInner, TResult> result)
        {
            IEnumerable<TResult> _result = Enumerable.Empty<TResult>();

            _result = from s in source
                      join i in inner
                      on pk(s) equals fk(i) into joinData
                      from left in joinData.DefaultIfEmpty()
                      select result(s, left);

            return _result;
        }

        public static IEnumerable<TResult> RightJoin<TSource, TInner, TKey, TResult>(this IEnumerable<TSource> source,
                                                  IEnumerable<TInner> inner,
                                                  Func<TSource, TKey> pk,
                                                  Func<TInner, TKey> fk,
                                                  Func<TSource, TInner, TResult> result)
        {
            IEnumerable<TResult> _result = Enumerable.Empty<TResult>();

            _result = from i in inner
                      join s in source
                      on fk(i) equals pk(s) into joinData
                      from right in joinData.DefaultIfEmpty()
                      select result(right, i);

            return _result;
        }
    }
}
