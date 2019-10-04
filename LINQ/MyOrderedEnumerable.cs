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

        public MyOrderedEnumerable(IEnumerable<T> source, IComparer<T> firstComparer, IComparer<T> secondComparer)
        {
            elements = new List<T>();
            Source = source;
            FirstComparer = firstComparer;
            SecondComparer = secondComparer;
        }

        private IEnumerable<T> Source { get; set; }
        private IComparer<T> FirstComparer { get; set; }
        private IComparer<T> SecondComparer { get; set; }

        public IOrderedEnumerable<T> CreateOrderedEnumerable<TKey>(Func<T, TKey> keySelector, IComparer<TKey> keyComparer, bool descending)
        {
            var secondComparer = new MyComparer<T, TKey>(keySelector, keyComparer);
            return new MyOrderedEnumerable<T>(this, FirstComparer, secondComparer);

        }

        public IEnumerator<T> GetEnumerator()
        {
            elements = Source.ToList();

            while (elements.Count > 0)
            {
                var minElement = elements[0];
                int minIndex = 0;
                for (int i = 1; i < elements.Count; i++)
                {
                    if (FirstComparer.Compare(elements[i], minElement) == 0 && SecondComparer != null)
                    {
                        if (SecondComparer.Compare(elements[i], minElement) == -1)
                        {
                            minElement = elements[i];
                            minIndex = i;
                        }

                    }
                    if (FirstComparer.Compare(elements[i], minElement) == -1)
                    {
                        minElement = elements[i];
                        minIndex = i;
                    }
                }
                elements.RemoveAt(minIndex);
                yield return minElement;
            }

        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
