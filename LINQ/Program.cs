
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

            var students = new List<Student>() {
            new Student(1, "John", "Computer Science"),
            new Student(2, "Steve", "Biology"),
            new Student(3, "Bill", "Math"),
            new Student(4, "Ram", "Biology"),
            new Student(5, "Ron", "Math"),
            new Student(6, "Ram", "Computer Science"),
        };
            Func<Student, string> nameSelector = x => x.Name;
            Func<Student, int> idSelector = x => x.ID;
            Func<Student, string> classSelector = x => x.Class;


            Console.WriteLine("Sorting by name, then by id, then by class");

            //var sorted = students.OrderBy(x => nameSelector(x)).
            //    ThenBy(y=>idSelector(y)).
            //    ThenBy(z=>classSelector(z));

            var sorted = LINQFunctions.OrderBy(students, x => nameSelector(x), new KeyComparer<string>()).
                ThenBy(y => idSelector(y), new KeyComparer<int>()).
                ThenBy(z => classSelector(z), new KeyComparer<string>());

            foreach (var current in sorted)
            {
                Console.WriteLine(current.ID + " : " + current.Name + " : " + current.Class);
            }

        }
    }
}
