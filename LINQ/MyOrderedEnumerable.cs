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

        public MyOrderedEnumerable(IEnumerable<T> source, List<IComparer<T>> comparers, int comparerIndex, bool isOrderBy)
        {
            Source = source;
            elements = Source.ToList();
            Comparers = comparers;
            CurrentComparerIndex = comparerIndex;
            IsOrderBy = isOrderBy;
        }

        public List<IComparer<T>> Comparers { get; set; }
        private IEnumerable<T> Source { get; set; }
        private int CurrentComparerIndex { get; set; }
        private bool IsOrderBy { get; set; }


        public IOrderedEnumerable<T> CreateOrderedEnumerable<TKey>(Func<T, TKey> keySelector, IComparer<TKey> keyComparer, bool descending)
        {
            var secondComparer = new MyComparer<T, TKey>(keySelector, keyComparer);
            Comparers.Add(secondComparer);
            return new MyOrderedEnumerable<T>(this, Comparers, CurrentComparerIndex + 1, false);
        }


        public IEnumerator<T> GetEnumerator()
        {

            var firstComparerIndex = CurrentComparerIndex;
            int secondComparerIndex = (CurrentComparerIndex + 1 < Comparers.Count) ? CurrentComparerIndex + 1 : -1;


            IComparer<T> FirstComparer = Comparers[firstComparerIndex];

            while (elements.Count > 0)
            {
                var minElement = elements[0];
                int minIndex = 0;

                for (int i = 1; i < elements.Count; i++)
                {
                    if (IsOrderBy)
                    {
                        if (FirstComparer.Compare(elements[i], minElement) == -1)
                        {
                            minElement = elements[i];
                            minIndex = i;
                        }
                    }

                    if (secondComparerIndex != -1 && FirstComparer.Compare(elements[i], minElement) == 0)
                    {
                        var SecondComparer = Comparers[secondComparerIndex];

                        if (SecondComparer.Compare(elements[i], minElement) == -1)
                        {
                            minElement = elements[i];
                            minIndex = i;
                        }
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
