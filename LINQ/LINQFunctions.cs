using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Collections;

namespace LINQ
{
    public static class LINQFunctions
    {
        public static IOrderedEnumerable<TSource> ThenBy<TSource, TKey>(
                                                        this IOrderedEnumerable<TSource> source,
                                                        Func<TSource, TKey> keySelector,
                                                        IComparer<TKey> comparer)
        {
            EnsureArgumentIsNotNull(source, nameof(source));
            EnsureArgumentIsNotNull(keySelector, nameof(keySelector));

            return source.CreateOrderedEnumerable(keySelector, comparer, false);
        }
        public static IOrderedEnumerable<TSource> OrderBy<TSource, TKey>(
                                                           this IEnumerable<TSource> source,
                                                           Func<TSource, TKey> keySelector,
                                                           IComparer<TKey> keyComparer)
        {
            EnsureArgumentIsNotNull(source, nameof(source));
            EnsureArgumentIsNotNull(keySelector, nameof(keySelector));


            var myComparer = new MyComparer<TSource, TKey>(keySelector, keyComparer);
            var comparersList = new List<IComparer<TSource>>();
            comparersList.Add(myComparer);
            return new MyOrderedEnumerable<TSource>(source, comparersList);
        }

        public static IEnumerable<TResult> GroupBy<TSource, TKey, TElement, TResult>(
                                                    this IEnumerable<TSource> source,
                                                    Func<TSource, TKey> keySelector,
                                                    Func<TSource, TElement> elementSelector,
                                                    Func<TKey, IEnumerable<TElement>, TResult> resultSelector,
                                                    IEqualityComparer<TKey> comparer)
        {
            EnsureArgumentIsNotNull(source, nameof(source));
            EnsureArgumentIsNotNull(keySelector, nameof(keySelector));
            EnsureArgumentIsNotNull(elementSelector, nameof(elementSelector));
            EnsureArgumentIsNotNull(resultSelector, nameof(resultSelector));
            EnsureArgumentIsNotNull(resultSelector, nameof(resultSelector));

            var dictionary = new Dictionary<TKey, List<TElement>>(comparer);


            foreach (var current in source)
            {
                var element = elementSelector(current);
                var key = keySelector(current);

                if (dictionary.ContainsKey(key))
                {
                    dictionary[key].Add(element);
                }
                else
                {
                    var newList = new List<TElement>() { element };
                    dictionary.Add(key, newList);
                }

            }

            foreach (var current in dictionary)
            {
                yield return resultSelector(current.Key, current.Value);
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
        public static IEnumerable<TSource> Except<TSource>(
                                                  this IEnumerable<TSource> first,
                                                  IEnumerable<TSource> second,
                                                  IEqualityComparer<TSource> comparer)
        {
            EnsureArgumentIsNotNull(first, nameof(first));
            EnsureArgumentIsNotNull(second, nameof(second));

            var hashset = new HashSet<TSource>(second, comparer);

            foreach (var x in first)
            {
                if (!hashset.Contains(x))
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
            EnsureArgumentIsNotNull(first, nameof(first));
            EnsureArgumentIsNotNull(second, nameof(second));

            var hashset = new HashSet<TSource>(second, comparer);

            foreach (var x in first)
            {
                if (hashset.Contains(x))
                {
                    yield return x;
                }
            }


        }
        public static IEnumerable<TSource> Union<TSource>(
                                                   this IEnumerable<TSource> first,
                                                   IEnumerable<TSource> second,
                                                   IEqualityComparer<TSource> comparer)
        {
            EnsureArgumentIsNotNull(first, nameof(first));
            EnsureArgumentIsNotNull(second, nameof(second));

            if (second is null)
            {
                throw new ArgumentNullException(nameof(second));
            }

            var joined = first.Concat(second);
            return Distinct(joined, comparer);

        }

        public static IEnumerable<TSource> Distinct<TSource>(
                                                    this IEnumerable<TSource> source,
                                                    IEqualityComparer<TSource> comparer)
        {
            EnsureArgumentIsNotNull(source, nameof(source));

            var hashset = new HashSet<TSource>(comparer);

            foreach (var current in source)
            {
                if (hashset.Add(current))
                {
                    yield return current;
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


            var dictionary = new Dictionary<TKey, TElement>();

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
