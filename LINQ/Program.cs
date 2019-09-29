
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

            Func<Student, string> elementSelector = x => x.Name;
            Func<Student, string> keySelector = x => x.Class;
            Func<string, IEnumerable<string>, KeyValuePair<string, IEnumerable<string>>> resultSelector = (Class, NameList) =>
            {
                {
                    return new KeyValuePair<string, IEnumerable<string>>(Class, NameList);
                }
            };

            var result = LINQFunctions.GroupBy(students,
                x => keySelector(x),
                y => elementSelector(y),
                (ClassName, NamesList) => resultSelector(ClassName, NamesList),
                new EqualityComparer<string>()
           ) ;

            foreach(var current in result)
            {
                Console.WriteLine(current.Key);
                foreach(var student in current.Value)
                {
                    Console.WriteLine(student);
                }
            }
        }
    }
}
