using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LINQ
{
    public class MyOrderedEnumerable<T> : IOrderedEnumerable<T>
    {
        private List<T> elements;

        public MyOrderedEnumerable(IEnumerable<T> source, IComparer<T> comparer)
        {
            elements = new List<T>();
            Comparer = comparer;
            Source = source;
        }

        private IEnumerable<T> Source { get; set; }
        private IComparer<T> Comparer { get; set; }

        public IOrderedEnumerable<T> CreateOrderedEnumerable<TKey>(Func<T, TKey> keySelector, IComparer<TKey> keyComparer, bool descending)
        {
            var secondComparer = new MyComparer<T, TKey>(keySelector, keyComparer);
            elements = this.ToList();
            Sort(secondComparer, elements);
            return new MyOrderedEnumerable<T>(elements, Comparer);

        }

        public void Sort(IComparer<T> comparer, List<T> elements)
        {
            var length = elements.Count();

            bool isSorted = false;
            while (!isSorted)
            {
                isSorted = true;
                for (int i = 0; i < length - 1; i++)
                {
                    for (int j = 0; j < length - i - 1; j++)
                    {
                        if (comparer.Compare(elements[j], elements[j + 1]) == 1)
                        {
                            var temp = elements[j];
                            elements[j] = elements[j + 1];
                            elements[j + 1] = temp;
                            isSorted = false;
                        }
                    }

                }
            }
        }
        public IEnumerator<T> GetEnumerator()
        {
            elements = Source.ToList();
            Sort(Comparer, elements);

            foreach (var current in elements)
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
