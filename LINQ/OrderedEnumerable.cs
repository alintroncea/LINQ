using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Linq;
namespace LINQ
{
    public class OrderedEnumerable<T> : IOrderedEnumerable<T>
    {
        private IEnumerable<object> source;
        private IComparer<object> comparer;

       
        public OrderedEnumerable(IEnumerable<T> source, IComparer<T> comparer)
        {
            Source = source;
            Comparer = comparer;
        }
        private IEnumerable<T> Source { get; set; }
        private IComparer<T> Comparer { get; set; }

        public IOrderedEnumerable<T> CreateOrderedEnumerable<TKey>(Func<T, TKey> keySelector, IComparer<TKey> comparer, bool descending)
        {
            return new OrderedEnumerable<T>(Source, Comparer);
        }

        public IEnumerator<T> GetEnumerator()
        {
            var enumerator = Source.GetEnumerator();

            while (enumerator.MoveNext())
            {
                yield return enumerator.Current;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
