using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Collections;

namespace LINQ
{
    public static class LINQFunctions
    {

        public static IEnumerable<TSource> Distinct<TSource>(
                                            this IEnumerable<TSource> source,
                                            IEqualityComparer<TSource> comparer)
        {
            EnsureArgumentIsNotNull(source, nameof(source));

           
            int i = 0;
            int k = i + 1;

            var firstEnumerator = source.GetEnumerator();

            while (firstEnumerator.MoveNext())
            {
                bool duplicate = false;
                var secondEnumerator = source.Skip(k++).GetEnumerator();

                while (secondEnumerator.MoveNext())
                {
                    if (comparer.Equals(firstEnumerator.Current, secondEnumerator.Current))
                    {
                        duplicate = true;
                    }
                }

                if (!duplicate)
                {
                    yield return firstEnumerator.Current;
                }
            }

        }

        public static IEnumerable<TResult> Join<TOuter, TInner, TKey, TResult>(
                                               this IEnumerable<TOuter> outer,
                                               IEnumerable<TInner> inner,
                                               Func<TOuter, TKey> outerKeySelector,
                                               Func<TInner, TKey> innerKeySelector,
                                               Func<TOuter, TInner, TResult> resultSelector)
        {
            EnsureArgumentIsNotNull(outer, nameof(outer));
            EnsureArgumentIsNotNull(inner, nameof(inner));
            EnsureArgumentIsNotNull(outerKeySelector, nameof(outerKeySelector));
            EnsureArgumentIsNotNull(innerKeySelector, nameof(innerKeySelector));
            EnsureArgumentIsNotNull(resultSelector, nameof(resultSelector));


            foreach (var x in outer)
            {
                var outerKey = outerKeySelector(x);

                foreach (var y in inner)
                {
                    var innerKey = innerKeySelector(y);

                    if (outerKey.Equals(innerKey))
                    {
                        yield return resultSelector(x, y);
                    }
                }
            }
        }
        public static TAccumulate Aggregate<TSource, TAccumulate>(
                                            this IEnumerable<TSource> source,
                                            TAccumulate seed,
                                            Func<TAccumulate, TSource, TAccumulate> func)
        {
            EnsureArgumentIsNotNull(source, nameof(source));
            EnsureArgumentIsNotNull(seed, nameof(seed));
            EnsureArgumentIsNotNull(func, nameof(func));


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

            EnsureArgumentIsNotNull(first, nameof(first));
            EnsureArgumentIsNotNull(second, nameof(second));


            IEnumerator<TFirst> firstEnumerator = first.GetEnumerator();
            IEnumerator<TSecond> secondEnumerator = second.GetEnumerator();

            for (int i = 0; i < shortestLength; i++)
            {
                while (firstEnumerator.MoveNext() && secondEnumerator.MoveNext())
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
            EnsureArgumentIsNotNull(source, nameof(source));
            EnsureArgumentIsNotNull(keySelector, nameof(keySelector));
            EnsureArgumentIsNotNull(elementSelector, nameof(source));


            Dictionary<TKey, TElement> dictionary = new Dictionary<TKey, TElement>();

            foreach (var current in source)
            {
                dictionary.Add(keySelector(current), elementSelector(current));
            }

            return dictionary;
        }

        public static IEnumerable<TResult> Select<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, TResult> selector)
        {
            EnsureArgumentIsNotNull(source, nameof(source));
            EnsureArgumentIsNotNull(selector, nameof(selector));


            foreach (var current in source)
            {
                yield return selector(current);
            }
        }

        public static IEnumerable<TSource> Where<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
        {
            EnsureArgumentIsNotNull(source, nameof(source));
            EnsureArgumentIsNotNull(predicate, nameof(predicate));

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
            EnsureArgumentIsNotNull(source, nameof(source));


            EnsureArgumentIsNotNull(selector, nameof(selector));


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
            EnsureArgumentIsNotNull(source, nameof(source));

            EnsureArgumentIsNotNull(predicate, nameof(predicate));


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
            EnsureArgumentIsNotNull(source, nameof(source));

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

            EnsureArgumentIsNotNull(source, nameof(source));


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

        public static void EnsureArgumentIsNotNull(object source, string name)
        {
            if (source is null)
            {
                throw new ArgumentNullException(name);
            }
        }
    }
}
