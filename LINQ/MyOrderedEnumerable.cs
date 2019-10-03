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
            FirstComparer = comparer;
            Source = source;
        }

        public IEnumerable<T> Source { get; set; }
        public IComparer<T> FirstComparer { get; set; }
        public IComparer<T> SecondComparer { get; set; }


        public IOrderedEnumerable<T> CreateOrderedEnumerable<TKey>(Func<T, TKey> keySelector, IComparer<TKey> comparer, bool descending)
        {

            SecondComparer = new MyComparer<T, TKey>(keySelector, comparer);

            var multipleComparer = new MultipleComparer<T>(FirstComparer, SecondComparer);
            return new MyOrderedEnumerable<T>(Source, multipleComparer);

        }

        public IEnumerator<T> GetEnumerator()
        {
            elements = Source.ToList();
            var length = elements.Count();
            
            bool isSorted = false;
            while (!isSorted)
            {
                int i;
                isSorted = true;
                for (i = 0; i < length - 1; i++)
                {
                    for (int j = 0; j < length - i - 1; j++)
                    {
                        if (FirstComparer.Compare(elements[j], elements[j + 1]) == 1)
                        {
                            var temp = elements[j];
                            elements[j] = elements[j + 1];
                            elements[j + 1] = temp;
                            isSorted = false;
                        }
                    }

                }
            }


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
