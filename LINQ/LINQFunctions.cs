using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
namespace LINQ
{
    public static class LINQFunctions
    {
        public static IEnumerable<TResult> Zip<TFirst, TSecond, TResult>(
                                               this IEnumerable<TFirst> first,
                                               IEnumerable<TSecond> second,
                                               Func<TFirst, TSecond, TResult> resultSelector)
        {
            var shortestLength = first.Count() < second.Count() ? first.Count() : second.Count();

            if (first is null)
            {
                ThrowArgumentNullException("first source");
            }

            if (second is null)
            {
                ThrowArgumentNullException("second source");
            }
            for (int i = 0; i < shortestLength; i++)
            {             
                yield return resultSelector(first.ElementAt(i), second.ElementAt(i));
            }
        }


        public static Dictionary<TKey, TElement> ToDictionary<TSource, TKey, TElement>(
                                                             this IEnumerable<TSource> source,
                                                             Func<TSource, TKey> keySelector,
                                                             Func<TSource, TElement> elementSelector)
        {
            if (source is null)
            {
                ThrowArgumentNullException("source");
            }

            if (keySelector is null)
            {
                ThrowArgumentNullException("key selector");
            }

            if (elementSelector is null)
            {
                ThrowArgumentNullException("element selector");
            }

            Dictionary<TKey, TElement> dictionary = new Dictionary<TKey, TElement>();

            foreach (var current in source)
            {
                if (keySelector(current) == null)
                {
                    ThrowArgumentNullException("current keySelector");
                }
                if (dictionary.ContainsKey(keySelector(current)))
                {
                    throw new ArgumentException("duplicate key");
                }

                dictionary.Add(keySelector(current), elementSelector(current));
            }

            return dictionary;
        }

        public static IEnumerable<TResult> Select<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, TResult> selector)
        {
            if (source is null)
            {
                ThrowArgumentNullException("source");
            }

            if (selector is null)
            {
                ThrowArgumentNullException("selector");
            }

            foreach (var current in source)
            {
                yield return selector(current);
            }
        }

        public static IEnumerable<TSource> Where<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
        {
            if (source is null)
            {
                ThrowArgumentNullException("source");
            }

            if (predicate is null)
            {
                ThrowArgumentNullException("predicate");
            }

            foreach (var current in source)
            {
                if (predicate(current))
                {
                    yield return current;
                }
            }
        }

        public static IEnumerable<TResult> SelectMany<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, IEnumerable<TResult>> selector)
        {
            if (source is null)
            {
                ThrowArgumentNullException("source");
            }

            if (selector is null)
            {
                ThrowArgumentNullException("selector");
            }

            foreach (var current in source)
            {
                var result = selector(current);

                foreach (var child in result)
                {
                    yield return child;
                }
            }
        }

        public static bool All<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
        {
            if(source is null)
            {
                ThrowArgumentNullException("source");
            }
            if (predicate is null)
            {
                ThrowArgumentNullException("predicate");
            }

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
            if (source is null)
            {
                ThrowArgumentNullException("source");
            }

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
            if (source is null)
            {
                ThrowArgumentNullException("source");
            }

            if(source.Count() == 0)
            {
                ThrowInvalidOperationException("source");
            }

            foreach (var element in source)
            {
                if (predicate(element))
                {
                    return element;
                }
            }

            throw new InvalidOperationException("No element has been found");
        }
        public static void ThrowArgumentNullException(string argument)
        {
            throw new ArgumentNullException("Argument " + argument + " is null.");
        }

        public static void ThrowInvalidOperationException(string argument)
        {
            throw new InvalidOperationException("Argument " + argument + " is empty.");
        }
    }
}
