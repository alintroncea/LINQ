using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace LINQ
{
    public class Comparer<T> : IComparer<T>
    {
        public int Compare(T x, T y)
        {
            return x.GetHashCode().CompareTo(y.GetHashCode());
        }
    }
}
