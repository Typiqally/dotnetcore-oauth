using System;
using System.Collections.Generic;
using System.Linq;

namespace NETCore.OAuth.Client.Extensions
{
    public static class CollectionExtensions
    {
        public static IEnumerable<IEnumerable<T>> Split<T>(this IEnumerable<T> enumerable, T separator, int skip = 1)
        {
            var enumerables = new List<IEnumerable<T>>();
            var list = enumerable.ToList();
            var count = 0;

            while (count < list.Count - 1)
            {
                var take = list.Skip(count);
                var items = take.TakeWhile(arg =>
                {
                    count += skip;
                    return !arg.Equals(separator);
                }).ToList();

                enumerables.Add(items);
                count += items.Count;
            }

            return enumerables;
        }

        public static void AddRange<T>(this ICollection<T> target, IEnumerable<T> source)
        {
            if (target == null) throw new ArgumentNullException(nameof(target));
            if (source == null) return;

            foreach (var element in source)
            {
                target.Add(element);
            }
        }
    }
}