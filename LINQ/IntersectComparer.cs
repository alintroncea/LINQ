using System;
using System.Collections.Generic;
using System.Text;

namespace LINQ
{
    public class IntersectComparer : IEqualityComparer<Employee>
    {
        public bool Equals(Employee x, Employee y)
        {
            if (x == null || y == null)
            {
                return false;
            }

            return x.FirstName.Equals(y.FirstName);
        }

        public int GetHashCode(Employee obj)
        {
            if (obj == null)
            {
                return 0;
            }
            return obj.FirstName.GetHashCode();
        }
    }
}
