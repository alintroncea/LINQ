
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
            int[] numbers = { 1, 4, 4, 1, 5 };

            var comparer = new Comparer<string>();


            var result = LINQFunctions.Distinct(strings, comparer);
            foreach (var current in result)
            {
                Console.WriteLine(current);
            }
        }
    }
}
