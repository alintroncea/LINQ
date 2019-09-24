
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
            int[] numbers = {1,4,3,1,299,5,299};
            var distinctEqualityComparer = new Comparer<int>();


            var result = LINQFunctions.Distinct(numbers, distinctEqualityComparer);

            foreach(var current in result)
            {
                Console.WriteLine(current);
            }
        }
    }
}
