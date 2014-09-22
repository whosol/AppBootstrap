using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhoSol.Utilities
{
    public static class SplitExtensions
    {
        public static IEnumerable<IEnumerable<T>> Split<T>(this IEnumerable<T> source, Func<T, bool> predicate)
        {
            List<T> currentList = new List<T>();
            foreach (var item in source)
            {
                if (currentList.Count > 0 && predicate(item))
                {
                    yield return currentList;
                    currentList = new List<T>();
                }
                currentList.Add(item);
            }

            if (currentList.Count > 0)
                yield return currentList;
        }

        public static IEnumerable<IEnumerable<T>> Split<T>(this T[] array, int size)
        {
            for (var i = 0; i < (float)array.Length / size; i++)
            {
                yield return array.Skip(i * size).Take(size);
            }
        }
    }
}
