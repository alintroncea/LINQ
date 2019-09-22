using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Collections;

namespace LINQ
{
    public static class LINQFunctions
    {
        public static IEnumerable<TSource> Except<TSource>(
                                                    this IEnumerable<TSource> first,
                                                    IEnumerable<TSource> second,
                                                    IEqualityComparer<TSource> comparer)
        {
            if (first is null)
            {
                throw new ArgumentNullException(nameof(first));
            }

            if (second is null)
            {
                throw new ArgumentNullException(nameof(second));
            }

            foreach (var x in first)
            {
                bool existsInSecond = false;
                foreach (var y in second)
                {
                    if (comparer.Equals(x, y))
                    {
                        existsInSecond = true;
                    }
                }
                if (!existsInSecond)
                {
                    yield return x;
                }
            }
        }

        public static IEnumerable<TSource> Intersect<TSource>(
                                           this IEnumerable<TSource> first,
                                           IEnumerable<TSource> second,
                                           IEqualityComparer<TSource> comparer)
        {

            if(first is null)
            {
                throw new ArgumentNullException(nameof(first));
            }

            if (second is null)
            {
                throw new ArgumentNullException(nameof(second));
            }

            foreach (var x in first)
            {
                foreach (var y in second)
                {
                    if (comparer.Equals(x, y))
                    {
                        yield return x;
                    }
                }
            }
        }
        public static IEnumerable<TSource> Union<TSource>(
                                            this IEnumerable<TSource> first,
                                            IEnumerable<TSource> second,
                                            IEqualityComparer<TSource> comparer)
        {
            if (first is null)
            {
                throw new ArgumentNullException(nameof(first));
            }
            if (second is null)
            {
                throw new ArgumentNullException(nameof(second));
            }

            HashSet<TSource> hashSet = new HashSet<TSource>(comparer);

            foreach (var current in first)
            {
                hashSet.Add(current);
            }

            foreach (var current in second)
            {
                hashSet.Add(current);
            }

            return hashSet;
        }

        public static IEnumerable<TSource> Distinct<TSource>(
                                            this IEnumerable<TSource> source,
                                            IEqualityComparer<TSource> comparer)
        {
            if(source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }
            HashSet<TSource> hashSet = new HashSet<TSource>(comparer);

            foreach (var current in source)
            {
                hashSet.Add(current);
            }
            return hashSet;
        }

        public static IEnumerable<TResult> Join<TOuter, TInner, TKey, TResult>(
                                                this IEnumerable<TOuter> outer,
                                                IEnumerable<TInner> inner,
                                                Func<TOuter, TKey> outerKeySelector,
                                                Func<TInner, TKey> innerKeySelector,
                                                Func<TOuter, TInner, TResult> resultSelector)
        {
            if (outer is null)
            {
                throw new ArgumentNullException(nameof(outer));
            }

            if (inner is null)
            {
                throw new ArgumentNullException(nameof(inner));
            }

            if (outerKeySelector is null)
            {
                throw new ArgumentNullException(nameof(outerKeySelector));
            }

            if (innerKeySelector is null)
            {
                throw new ArgumentNullException(nameof(innerKeySelector));
            }

            if (resultSelector is null)
            {
                throw new ArgumentNullException(nameof(resultSelector));
            }

            var shortestLength = Math.Min(outer.Count(), inner.Count());

            IEnumerator<TOuter> outerEnumerator = outer.GetEnumerator();
            IEnumerator<TInner> innerEnumerator = inner.GetEnumerator();

            for (int i = 0; i < shortestLength; i++)
            {
                while(outerEnumerator.MoveNext() && innerEnumerator.MoveNext())
                {
                    var firstKey = outerKeySelector(outerEnumerator.Current);
                    var secondKey = innerKeySelector(innerEnumerator.Current);

                    if (firstKey.Equals(secondKey))
                    {
                        yield return resultSelector(outerEnumerator.Current, innerEnumerator.Current);
                    }
                }             
            }
        }
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
                throw new ArgumentNullException(nameof(first));
            }

            if (second is null)
            {
                throw new ArgumentNullException(nameof(second));
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
                throw new ArgumentNullException(nameof(source));
            }

            if (keySelector is null)
            {
                throw new ArgumentNullException(nameof(keySelector));
            }


            if (elementSelector is null)
            {
                throw new ArgumentNullException(nameof(elementSelector));
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
                throw new ArgumentNullException(nameof(source));
            }

            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
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
                throw new ArgumentNullException(nameof(source));
            }

            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
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
                throw new ArgumentNullException(nameof(source));
            }

            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
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
                throw new ArgumentNullException(nameof(source));
            }

            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
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
                throw new ArgumentNullException(nameof(source));
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
                throw new ArgumentNullException(nameof(source));
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
      
    }
}
