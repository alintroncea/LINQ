using System;
using System.Collections.Generic;
using System.Text;

namespace LINQ
{
    public class KeyComparer<T> : IComparer<T>
    {
        public int Compare(T x, T y)
        {
            IComparable Ic1 = (IComparable)x;
            IComparable Ic2 = (IComparable)y;

            return Ic1.CompareTo(Ic2);
        }
    }
}
