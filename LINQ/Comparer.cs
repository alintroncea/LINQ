using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace LINQ
{
    public class Comparer<T> : IComparer<Student>
    {
        public int Compare(Student x, Student y)
        {
            return x.ID.CompareTo(y.ID);
        }
    }
}
