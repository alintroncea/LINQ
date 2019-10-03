
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




            string[] capitals = {  "Berlin",
                                    "Paris",
                                    "Madrid",
                                    "Tokyo",
                                    "London",
                                    "Athens",
                                    "Beijing",
                                    "Seoul" };

            Func<string, int> capitalLength = (x) => x.Length;
            Func<string, string> capital = (x) => x;
            var result = LINQFunctions.OrderBy(capitals, x => capitalLength(x), new KeyComparer<int>()).ThenBy(x=>capital(x), new KeyComparer<string>());

            foreach (var current in result)
            {
                Console.WriteLine(current);
            }

        }
    }
}
