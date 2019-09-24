
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
            string[] strings = { "Mara", "Mara", "Ana", "Andreea" };

            var distinctEqualityComparer = new Comparer<string>();


            var result = LINQFunctions.Distinct(strings, distinctEqualityComparer);

            foreach(var current in result)
            {
                Console.WriteLine(current);
            }
        }
    }
}
