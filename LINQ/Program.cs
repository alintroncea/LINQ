
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace LINQ
{
    static class Program
    {
        static void Main(string[] args)
        {

            //string[] strings1 = { "Mara","Ana", "Andreea" };
            //string[] strings2 = { "Mara", "Ioana", "Mihaela", "Andreea" };

            //int[] numbers1 = { 1, 4, 3, 9, 8 };

            //int[] numbers2 = { 1, 4, 4, 1, 5 };

            var students = Student.GetSudents();

            Func<Student, int> myFunc = (x) => x.ID;
            var comparer = new Comparer<int>();
            var ordered = new OrderedEnumerable<Student>(students);

            var result = LINQFunctions.OrderBy(students, x=>myFunc(x), comparer);


            foreach (var current in result)
            {
                Console.WriteLine("NAME: " + current.Name + " ID: " + current.ID);
            }
        }
    }
}
