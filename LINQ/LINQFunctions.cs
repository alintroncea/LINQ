using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Collections;

namespace LINQ
{
    public static class LINQFunctions
    {
        public static TAccumulate Aggregate<TSource, TAccumulate>(
                                            this IEnumerable<TSource> source,
                                            TAccumulate seed,
                                            Func<TAccumulate, TSource, TAccumulate> func)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (seed == null)
            {
                throw new ArgumentNullException(nameof(seed));
            }


            if (func is null)
            {
                throw new ArgumentNullException(nameof(func));
            }

            TAccumulate result = seed;

            foreach (var element in source)
            {
                result = func(result, element);
            }

            return result;
        }

        public static IEnumerable<TResult> Zip<TFirst, TSecond, TResult>(
                                               this IEnumerable<TFirst> first,
                                               IEnumerable<TSecond> second,
                                               Func<TFirst, TSecond, TResult> resultSelector)
        {
            var shortestLength = Math.Min(first.Count(), second.Count());

            if (first is null)
            {
                EnsureArgumentIsNotNull(nameof(first));
            }

            if (second is null)
            {
                EnsureArgumentIsNotNull(nameof(second));
            }

            IEnumerator<TFirst> firstEnumerator = first.GetEnumerator();
            IEnumerator<TSecond> secondEnumerator = second.GetEnumerator();
         
            for (int i = 0; i < shortestLength; i++)
            {
                while(firstEnumerator.MoveNext() && secondEnumerator.MoveNext())
                {
                    yield return resultSelector(firstEnumerator.Current, secondEnumerator.Current);
                }
            }
        }


        public static Dictionary<TKey, TElement> ToDictionary<TSource, TKey, TElement>(
                                                             this IEnumerable<TSource> source,
                                                             Func<TSource, TKey> keySelector,
                                                             Func<TSource, TElement> elementSelector)
        {
            if (source is null)
            {
                EnsureArgumentIsNotNull(nameof(source));
            }

            if (keySelector is null)
            {
                EnsureArgumentIsNotNull(nameof(keySelector));
            }


            if (elementSelector is null)
            {
                EnsureArgumentIsNotNull(nameof(elementSelector));
            }

            Dictionary<TKey, TElement> dictionary = new Dictionary<TKey, TElement>();

            foreach (var current in source)
            {               
                dictionary.Add(keySelector(current), elementSelector(current));
            }

            return dictionary;
        }

        public static IEnumerable<TResult> Select<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, TResult> selector)
        {
            if (source is null)
            {
                EnsureArgumentIsNotNull(nameof(source));
            }

            if (selector is null)
            {
                EnsureArgumentIsNotNull(nameof(selector));
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
                EnsureArgumentIsNotNull(nameof(source));
            }

            if (predicate is null)
            {
                EnsureArgumentIsNotNull(nameof(predicate));
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
                EnsureArgumentIsNotNull(nameof(source));
            }

            if (selector is null)
            {
                EnsureArgumentIsNotNull(nameof(selector));
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
            if (source is null)
            {
                EnsureArgumentIsNotNull(nameof(source));
            }

            if (predicate is null)
            {
                EnsureArgumentIsNotNull(nameof(predicate));
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
                EnsureArgumentIsNotNull(nameof(source));
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
                EnsureArgumentIsNotNull(nameof(source));
            }

            if (source.Count() == 0)
            {
                throw new InvalidOperationException();
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
      
        public static void EnsureArgumentIsNotNull(string source)
        {
            throw new ArgumentNullException(source);
        }
    }
}
