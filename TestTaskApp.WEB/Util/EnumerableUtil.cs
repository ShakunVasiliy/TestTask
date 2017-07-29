using System;
using System.Collections.Generic;

namespace TestTaskApp.WEB.Util
{
    public static class EnumerableUtil
    {
        public static IEnumerable<T> AddToFirstPosition<T>(this IEnumerable<T> oldCollection, T obj)
            where T : class
        {
            var newCollection = new List<T>() { obj };
            newCollection.AddRange(oldCollection);

            return newCollection;
        }
    }
}