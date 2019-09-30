using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Linq;
namespace LINQ
{
    public class OrderedEnumerable<T> : IOrderedEnumerable<T>
    {

        public OrderedEnumerable(IEnumerable<T> source)
        {
            Source = source;
        }

        private IEnumerable<T> Source { get; set; }

        public IOrderedEnumerable<T> CreateOrderedEnumerable<TKey>(Func<T, TKey> keySelector, IComparer<TKey> comparer, bool descending)
        {
            var list = new List<T>(Source.Select(x => x));       
            var length = list.Count();

            bool isSorted = false;
            while (!isSorted)
            {
                isSorted = true;
                for (int i = 0; i < length - 1; i++)
                {
                    for (int j = 0; j < length - i - 1; j++)
                    {
                        var firstKey = keySelector(list[j]);
                        var secondKey = keySelector(list[j + 1]);

                        if (comparer.Compare(firstKey, secondKey) == 1)
                        {
                            var temp = list[j];
                            list[j] = list[j + 1];
                            list[j + 1] = temp;
                            isSorted = false;
                        }
                    }
                }
            }
            return new OrderedEnumerable<T>(list);
        }

        public IEnumerator<T> GetEnumerator()
        {
            foreach (var current in Source)
            {
                yield return current;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

    }
}
