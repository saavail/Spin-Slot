using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Utilities
{
    public static class LinqExtensions
    {
        public static T RandomElement<T>(this IList<T> list)
        {
            if (list == null || list.Count == 0)
            {
                return default(T);
            }
            return list[UnityEngine.Random.Range(0, list.Count)];
        }
        
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> list)
        {
            return list == null || !list.Any();
        }
        
        public static bool Contains<T>(this T[] self, T obj)
        {
            return System.Array.IndexOf(self, obj) != -1;
        }
        
        public static T Max<T, TR>(this IEnumerable<T> source, Func<T, TR> selector)
        {
            if (source == null)
            {
                Debug.LogError("Enumerable is null.");
                return default(T);
            }

            var @default = Comparer<TR>.Default;
            T y = default(T);
            if ((object)y == null)
            {
                foreach (T x in source)
                {
                    if ((object)x != null && ((object)y == null || @default.Compare(selector(x), selector(y)) > 0))
                        y = x;
                }
                return y;
            }
            else
            {
                using (IEnumerator<T> enumerator = source.GetEnumerator())
                {
                    if (!enumerator.MoveNext())
                        return default(T);

                    y = enumerator.Current;
                    while (enumerator.MoveNext())
                    {
                        T x = enumerator.Current;
                        if (@default.Compare(selector(x), selector(y)) > 0)
                            y = x;
                    }
                    return y;
                }
            }
        }
        
        public static T Min<T, TR>(this IEnumerable<T> source, Func<T, TR> selector)
        {
            if (source == null)
            {
                Debug.LogError("Enumerable is null.");
                return default(T);
            }

            var @default = Comparer<TR>.Default;
            T y = default(T);
            if ((object)y == null)
            {
                foreach (T x in source)
                {
                    if ((object)x != null && ((object)y == null || @default.Compare(selector(x), selector(y)) < 0))
                        y = x;
                }
                return y;
            }
            else
            {
                using (IEnumerator<T> enumerator = source.GetEnumerator())
                {
                    if (!enumerator.MoveNext())
                        return default(T);

                    y = enumerator.Current;
                    while (enumerator.MoveNext())
                    {
                        T x = enumerator.Current;
                        if (@default.Compare(selector(x), selector(y)) < 0)
                            y = x;
                    }
                    return y;
                }
            }
        }
    }
}