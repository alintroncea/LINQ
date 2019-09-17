using System;
using Xunit;

namespace LINQ
{
    public class TestLINQFunctions
    {
        [Fact]
        public void TestSelect()
        {
            var employees = Employee.GetEmployees();

            Func<Employee, Employee> myFunc = (x) => x.FirstName.StartsWith('P') ? x : null;


            var selectedEmployees = LINQFunctions.Select(employees, p => myFunc(p));

            foreach (var current in selectedEmployees)
            {
                Assert.True(current.FirstName.StartsWith('P'));
            }

        }

        [Fact]
        public void TestAllWhenTrue()
        {
            var array = new int[] { 2, 4, 6 };

            Func<int, bool> myFunc = (x) => { return x % 2 == 0; };

            Assert.True(LINQFunctions.All(array, p => myFunc(p)));
        }

        [Fact]
        public void TestAllWhenFalse()
        {
            var array = new int[] { 2, 4, 3 };

            Func<int, bool> myFunc = (x) => { return x % 2 == 0; };

            Assert.False(LINQFunctions.All(array, p => myFunc(p)));
        }

        [Fact]
        public void TestAnyWhenTrue()
        {
            var array = new string[] { "Maria", "Andreea", "Cristina" };

            Func<string, bool> myFunc = (x) => { return x.Length == 5; };

            Assert.True(LINQFunctions.Any(array, p => myFunc(p)));
        }

        [Fact]
        public void TestAnyWhenFalse()
        {
            var array = new string[] { "Ana", "Andreea", "Cristina" };

            Func<string, bool> myFunc = (x) => { return x.Length == 5; };

            Assert.False(LINQFunctions.Any(array, p => myFunc(p)));
        }

        [Fact]
        public void TestFirstWhenExists()
        {
            var array = new int[] { 2, 4, 3 };

            Func<int, bool> myFunc = (x) => { return x == 4; };

            var result = LINQFunctions.First(array, p => myFunc(p));

            Assert.Equal(4, result);
        }

        [Fact]
        public void TestFirstWhenDoesntExists()
        {
            var array = new int[] { 2, 5, 3 };

            Func<int, bool> myFunc = (x) => { return x == 4; };

            Assert.Throws<InvalidOperationException>(() => LINQFunctions.First(array, p => myFunc(p)));
        }
    }
}
