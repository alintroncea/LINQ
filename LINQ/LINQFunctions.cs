using System;
using System.Collections.Generic;
using System.Text;

namespace LINQ
{
    public static class LINQFunctions
    {

        public static IEnumerable<TResult> Select<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, TResult> selector)
        {
            foreach (var current in source)
            {
                yield return selector(current);
            }

        }   

    public static bool All<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
    {
        foreach (var element in source)
        {
            if (!predicate(element))
            {
                return false;
            }
        }

        return true;
    }

    public static bool Any<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
    {
        foreach (var element in source)
        {
            if (predicate(element))
            {
                return true;
            }
        }

        return false;
    }

    public static TSource First<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
    {
        foreach (var element in source)
        {
            if (predicate(element))
            {
                return element;
            }
        }

        throw new InvalidOperationException("No element has been found");
    }

}
}
