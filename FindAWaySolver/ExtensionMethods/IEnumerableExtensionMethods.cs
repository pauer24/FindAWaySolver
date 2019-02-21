using System;
using System.Collections.Generic;

namespace FindAWaySolver.ExtensionMethods
{
    internal static class IEnumerableExtensionMethods
    {
        public static void ForEach<T>(this IEnumerable<T> enumeration, Action<T> action)
        {
            foreach (T item in enumeration)
            {
                action(item);
            }
        }

        public static (int Index, T Item) FirstIndexOf<T>(this IEnumerable<T> enumerable, Predicate<T> condition)
        {
            var index = 0; 
            foreach(var item in enumerable)
            {
                if (condition(item))
                {
                    return (index, item);
                }

                index++;
            }

            return (-1, default(T));
        }
    }
}