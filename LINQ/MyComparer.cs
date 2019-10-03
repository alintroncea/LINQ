using System;
using System.Collections.Generic;
using System.Text;

namespace LINQ
{
    public class MyComparer<TSource, TKey> : IComparer<TSource>
    {
        public MyComparer(Func<TSource, TKey> keySelector, IComparer<TKey> comparer)
        {
            KeySelector = keySelector;
            Comparer = comparer;
        }
        private Func<TSource, TKey> KeySelector { get; set; }

        private IComparer<TKey> Comparer { get; set; }

        public int Compare(TSource x, TSource y)
        {
            return Comparer.Compare(KeySelector(x), KeySelector(y));
        }
       
    }
}
