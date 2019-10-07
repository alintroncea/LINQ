
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
            new Student(5, "John", "Computer Science"),
            new Student(4, "Steve", "Biology"),
            new Student(1, "Bill", "Math"),
            new Student(4, "Ram", "Math"),
            new Student(3, "Ron", "Math"),
            new Student(4, "Ram", "Biology"),
        };

            Func<Student, int> idFunc = (x) => x.ID;
            Func<Student, string> nameFunc = (x) => x.Name;
            Func<Student, string> classFunc = (x) => x.Class;

            //var result = students.OrderBy(x=>idFunc(x)).ThenBy(y=>nameFunc(y));

            var result = LINQFunctions.OrderBy(students, x => idFunc(x), new KeyComparer<int>())
            .ThenBy(y => nameFunc(y), new KeyComparer<string>())
            .ThenBy(z => classFunc(z), new KeyComparer<string>());

            foreach (var current in result)
            {
                Console.WriteLine(current.ID + " : " + current.Name + " : " + current.Class);
            }

        }
    }
}
