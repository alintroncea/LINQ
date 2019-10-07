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
            Source = source;
            elements = Source.ToList();
            FirstComparer = firstComparer;
            SecondComparer = secondComparer;
        }

        public IComparer<T> FirstComparer { get; set; }
        public IComparer<T> SecondComparer { get; set; }
        private IEnumerable<T> Source { get; set; }


        public IOrderedEnumerable<T> CreateOrderedEnumerable<TKey>(Func<T, TKey> keySelector, IComparer<TKey> keyComparer, bool descending)
        {
            var secondComparer = new MyComparer<T, TKey>(keySelector, keyComparer);
            return new MyOrderedEnumerable<T>(this, FirstComparer, secondComparer);
        }


        public IEnumerator<T> GetEnumerator()
        {

            while (elements.Count > 0)
            {
                var minElement = elements[0];
                int minIndex = 0;

                for (int i = 1; i < elements.Count; i++)
                {
                    if (IsOrderBy.Check && FirstComparer.Compare(elements[i], minElement) == -1)
                    {
                        minElement = elements[i];
                        minIndex = i;
                    }

                    if (!IsOrderBy.Check && FirstComparer.Compare(elements[i], minElement) == 0)
                    {

                        if (SecondComparer != null && SecondComparer.Compare(elements[i], minElement) == -1)
                        {
                            minElement = elements[i];
                            minIndex = i;
                        }

                    }

                }
                elements.RemoveAt(minIndex);
                yield return minElement;
            }
            if (SecondComparer != null)
            {
                FirstComparer = SecondComparer;
                IsOrderBy.Check = false;
            }
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
