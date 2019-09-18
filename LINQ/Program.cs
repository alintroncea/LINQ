
using System;
using System.Collections.Generic;
using System.Linq;

namespace LINQ
{
    static class Program
    {
       static void Main(string[] args)
        {
            var employees = Employee.GetEmployees();
            Func<Employee, bool> myFunc = (x) => x.FirstName.StartsWith('P');

            var selectedEmployees = LINQFunctions.Where(employees, p=>myFunc(p));

            foreach(var current in selectedEmployees)
            {
                Console.WriteLine(current.FirstName);
            }

        }

    }
}
