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

        public MyOrderedEnumerable(IEnumerable<T> source, List<IComparer<T>> comparers)
        {
            Source = source;
            elements = Source.ToList();
            Comparers = comparers;
        }

        public List<IComparer<T>> Comparers { get; set; }
        private IEnumerable<T> Source { get; set; }


        public IOrderedEnumerable<T> CreateOrderedEnumerable<TKey>(Func<T, TKey> keySelector, IComparer<TKey> keyComparer, bool descending)
        {
            var secondComparer = new MyComparer<T, TKey>(keySelector, keyComparer);
            Comparers.Add(secondComparer);
            return new MyOrderedEnumerable<T>(this, Comparers);
        }


        public IEnumerator<T> GetEnumerator()
        {
            while (elements.Count > 0)
            {
                var minElement = elements[0];
                int minIndex = 0;


                for (int i = 1; i < elements.Count; i++)
                {
                    for (int x = 0; x < Comparers.Count; x++)
                    {
                        for (int z = x + 1; z < Comparers.Count; z++)
                        {
                            var FirstComparer = Comparers[x];
                            var SecondComparer = Comparers[z];

                            if (FirstComparer.Compare(elements[i], minElement) == -1)
                            {
                                minElement = elements[i];
                                minIndex = i;
                            }
                            if (FirstComparer.Compare(elements[i], minElement) == 0)
                            {
                                if (SecondComparer.Compare(elements[i], minElement) == -1)
                                {
                                    minElement = elements[i];
                                    minIndex = i;
                                }
                            }
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
