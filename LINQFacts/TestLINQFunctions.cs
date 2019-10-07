using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace LINQ
{
    public class TestLINQFunctions
    {
        [Fact]
        public void TestThenByWithTwoCriteria()
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

            var result = LINQFunctions.OrderBy(students, x => idFunc(x), new KeyComparer<int>()).
                ThenBy(y => nameFunc(y), new KeyComparer<string>()).ToList();

            Assert.Equal(1, result[0].ID);
            Assert.Equal(5, result[5].ID);
            Assert.Equal("Steve", result[4].Name);

        }

        [Fact]
        public void TestThenByWithOneCriterion()
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

            var result = LINQFunctions.OrderBy(students, x => idFunc(x), new KeyComparer<int>()).
                ThenBy(y => nameFunc(y), new KeyComparer<string>()).ToList();

            Assert.Equal(1, result[0].ID);
            Assert.Equal(5, result[5].ID);
            Assert.Equal("Steve", result[4].Name);

        }
        [Fact]
        public void TestOrderBy()
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

            var result = students.OrderBy(x => nameSelector(x));

            Assert.Equal("Steve", result.Last().Name);
        }
        [Fact]
        public void TestGroupBy()
        {
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
           );


            Assert.Equal(2, result.Count());
        }
        [Fact]
        public void TestExcept()
        {
            string[] firstArray = { "Andreea", "Maria", "Ioana" };
            string[] secondArray = { "Andreea", "Maria", "Andrei", "Ionut" };


            var exceptComparer = new EqualityComparer<string>();
            var result = LINQFunctions.Except(firstArray, secondArray, exceptComparer);

            Assert.Equal(1, result.Count());
        }
        [Fact]
        public void TestExceptWhenThrowingExceptions()
        {
            string[] firstArray = { "Andreea", "Maria", "Ioana" };
            string[] secondArray = null;

            var result = LINQFunctions.Except(firstArray, secondArray, new EqualityComparer<string>());
            Assert.Throws<ArgumentNullException>(() => result.Count());
        }

        [Fact]
        public void TestIntersect()
        {
            List<Employee> oldEmployees = new List<Employee>
            {
                new Employee {ID = 104,
                    FirstName = "Ionut",
                    LastName = "Popescu",
                    Salary = 90000,
                    Departments = { new Department { Name = "Marketing" }, new Department {Name = "Sales" } } },

                new Employee {ID = 105,
                    FirstName = "Andreea",
                    LastName = "Popescu",
                    Salary = 100000,
                    Departments ={ new Department { Name = "Advertisement" }, new Department {Name = "Production" } } },

                new Employee { ID = 106,
                    FirstName = "Mihai",
                    LastName = "Andreescu",
                    Salary = 160000,
                    Departments = { new Department { Name = "Production" }, new Department {Name = "Sales" } } }
                };

            List<Employee> newEmployees = new List<Employee>
            {
                new Employee {ID = 104,
                    FirstName = "George",
                    LastName = "Petrescu",
                    Salary = 90000,
                    Departments = { new Department { Name = "Marketing" }, new Department {Name = "Sales" } } },

                new Employee {ID = 105,
                    FirstName = "Ionut",
                    LastName = "Ghita",
                    Salary = 100000,
                    Departments ={ new Department { Name = "Advertisement" }, new Department {Name = "Production" } } },

                new Employee { ID = 106,
                    FirstName = "Ioana",
                    LastName = "Mihaescu",
                    Salary = 160000,
                    Departments = { new Department { Name = "Production" }, new Department {Name = "Sales" } } }
                };

            var intersectComparer = new IntersectComparer();
            var result = LINQFunctions.Intersect(oldEmployees, newEmployees, intersectComparer);

            Assert.Equal(1, result.Count());
        }
        [Fact]
        public void TestIntersectWhenThrowingExceptions()
        {
            List<Employee> oldEmployees = null;

            List<Employee> newEmployees = new List<Employee>
            {
                new Employee {ID = 104,
                    FirstName = "George",
                    LastName = "Petrescu",
                    Salary = 90000,
                    Departments = { new Department { Name = "Marketing" }, new Department {Name = "Sales" } } },

                new Employee {ID = 105,
                    FirstName = "Ionut",
                    LastName = "Ghita",
                    Salary = 100000,
                    Departments ={ new Department { Name = "Advertisement" }, new Department {Name = "Production" } } },

                new Employee { ID = 106,
                    FirstName = "Ioana",
                    LastName = "Mihaescu",
                    Salary = 160000,
                    Departments = { new Department { Name = "Production" }, new Department {Name = "Sales" } } }
                };


            var result = LINQFunctions.Intersect(oldEmployees, newEmployees, new IntersectComparer());
            Assert.Throws<ArgumentNullException>(() => result.Count());
        }
        [Fact]
        public void TestUnion()
        {
            string[] oldEmployees = { "Andreea", "Maria", "Ioana" };
            string[] newEmployees = { "Andreea", "George", "Andrei", "Ionut" };

            var unionComparer = new EqualityComparer<string>();
            var result = LINQFunctions.Union(oldEmployees, newEmployees, unionComparer);

            int counter = 0;

            foreach (var current in result)
            {
                if (current.Equals("Andreea"))
                {
                    counter++;
                }
            }

            Assert.Equal(1, counter);
            Assert.Equal(6, result.Count());
        }
        [Fact]
        public void TestUnionWhenThrowingExceptions()
        {
            string[] oldEmployees = null;
            string[] newEmployees = { "Andreea", "George", "Andrei", "Ionut" };

            var unionComparer = new EqualityComparer<string>();
            Assert.Throws<ArgumentNullException>(() => LINQFunctions.Union(oldEmployees, newEmployees, unionComparer));

        }
        [Fact]
        public void TestDistinct()
        {
            string[] employeesNames = { "Mara", "Mara", "Ana", "Andreea" };
            var distinctEqualityComparer = new EqualityComparer<string>();


            var result = LINQFunctions.Distinct(employeesNames, distinctEqualityComparer);

            Assert.Equal(3, result.Count());
        }
        [Fact]
        public void TestDistinctWhenThrowingExceptions()
        {
            string[] employeesNames = null;

            var result = LINQFunctions.Distinct(employeesNames, new EqualityComparer<string>());
            Assert.Throws<ArgumentNullException>(() => result.Count());
        }
        [Fact]
        public void TestJoin()
        {
            Person magnus = new Person { Name = "Hedlund, Magnus" };
            Person terry = new Person { Name = "Adams, Terry" };
            Person charlotte = new Person { Name = "Weiss, Charlotte" };

            Pet barley = new Pet { Name = "Barley", Owner = terry };
            Pet boots = new Pet { Name = "Boots", Owner = terry };
            Pet whiskers = new Pet { Name = "Whiskers", Owner = charlotte };
            Pet daisy = new Pet { Name = "Daisy", Owner = magnus };

            List<Person> people = new List<Person> { magnus, terry, charlotte };
            List<Pet> pets = new List<Pet> { barley, boots, whiskers, daisy };

            Func<Person, Person> outerFunc = (person) => person;
            Func<Pet, Person> innerFunc = (pet) => pet.Owner;

            Func<Person, Pet, KeyValuePair<Person, Pet>> selector = (person, pet) =>
            {
                {
                    return new KeyValuePair<Person, Pet>(person, pet);
                }
            };


            var query = people.Join(pets, x => outerFunc(x), y => innerFunc(y), (person, pet) => selector(person, pet));

            var kvp1 = new KeyValuePair<Person, Pet>(magnus, daisy);

            Assert.True(query.Contains(kvp1));

        }
        [Fact]
        public void TestJoinWhenThrowingExeptions()
        {
            List<Employee> employees = null;
            var departmentList = new List<Department>
           {
               new Department{ DepartmentID = 1, Name ="HR"},
               new Department{DepartmentID = 2, Name ="Marketing"},
               new Department{DepartmentID = 4, Name ="Sales"}
           };

            Func<Employee, int> outerFunc = (employee) => employee.ID;
            Func<Department, int> innerFunc = (department) => department.DepartmentID;

            Func<Employee, Department, KeyValuePair<string, string>> selector = (employee, department) =>
            {
                {
                    return new KeyValuePair<string, string>(employee.FirstName, department.Name);
                }
            };

            var result = LINQFunctions.Join(employees, departmentList,
                                        employee => outerFunc(employee), department => innerFunc(department),
                                        (employee, department) =>
                                        selector(employee, department));

            Assert.Throws<ArgumentNullException>(() => result.Count());
        }
        [Fact]
        public void TestAggregate()
        {
            int[] array = { 1, 2, 4, 5 };

            Func<int, int, int> myFunc = (x, z) => x * z;

            var result = LINQFunctions.Aggregate(array, 5, (a, b) => myFunc(a, b));

            Assert.Equal(200, result);
        }
        [Fact]
        public void TestAggregateWhenThrowingExceptions()
        {
            int[] array = null;
            Func<int, int, int> myFunc = (x, z) => x * z;

            Assert.Throws<ArgumentNullException>(() => LINQFunctions.Aggregate(array, 5, (a, b) => myFunc(a, b)));
        }
        [Fact]
        public void TestZip()
        {
            int[] numbers = { 1, 2, 3, 4 };
            string[] words = { "one", "two", "three", "nine", "six" };

            var result = LINQFunctions.Zip(numbers, words, (first, second) => first + " " + second);

            Assert.True(result.Contains("3 three"));
        }

        [Fact]
        public void TestZipWhenThrowingExceptions()
        {
            int[] numbers = null;
            string[] words = { "one", "two", "three", "nine", "six" };

            var result = LINQFunctions.Zip(numbers, words, (first, second) => first + " " + second);

            Assert.Throws<ArgumentNullException>(() => result.Count());

        }

        [Fact]
        public void TestToDictionary()
        {
            var employees = Employee.GetEmployees();

            Func<Employee, int> myKeyFunc = (x) => x.ID;
            Func<Employee, string> myElementFunc = (x) => x.FirstName;

            var dictionary = LINQFunctions.ToDictionary(employees, p => myKeyFunc(p), z => myElementFunc(z));

            var kvp1 = new KeyValuePair<int, string>(104, "Anurag");
            var kvp2 = new KeyValuePair<int, string>(105, "Sambit");
            var kvp3 = new KeyValuePair<int, string>(106, "Sushanta");
            var kvp4 = new KeyValuePair<int, string>(101, "Preety");
            var kvp5 = new KeyValuePair<int, string>(103, "Hina");
            var kvp6 = new KeyValuePair<int, string>(102, "Priyanka");

            var kvp7 = new KeyValuePair<int, string>(109, "Andrei");

            Assert.True(dictionary.Contains(kvp1));
            Assert.True(dictionary.Contains(kvp2));
            Assert.True(dictionary.Contains(kvp3));
            Assert.True(dictionary.Contains(kvp4));
            Assert.True(dictionary.Contains(kvp5));
            Assert.True(dictionary.Contains(kvp6));

            Assert.False(dictionary.Contains(kvp7));
        }

        [Fact]
        public void TestToDictionaryWhenThrowingExceptions()
        {
            List<Employee> employees = null;

            Func<Employee, int> myKeyFunc = (x) => x.ID;
            Func<Employee, string> myElementFunc = (x) => x.FirstName;

            Assert.Throws<ArgumentNullException>(() => LINQFunctions.ToDictionary(employees, p => myKeyFunc(p), z => myElementFunc(z)));

            employees = new List<Employee>();

            var firstEmployee = new Employee() { ID = 1, FirstName = "Andrei", LastName = "Popescu" };
            var secondEmployee = new Employee() { ID = 1, FirstName = "Mihai", LastName = "Andreescu" };

            employees.Add(firstEmployee);
            employees.Add(secondEmployee);

            Assert.Throws<ArgumentException>(() => LINQFunctions.ToDictionary(employees, p => myKeyFunc(p), z => myElementFunc(z)));
        }

        [Fact]
        public void TestSelect()
        {
            var employees = Employee.GetEmployees();

            Func<Employee, bool> myFunc = (x) => x.FirstName.StartsWith('P');
            var selectedEmployees = LINQFunctions.Select(employees, p => myFunc(p));

            int counter = 0;

            foreach (var current in selectedEmployees)
            {
                if (current)
                {
                    counter++;
                }
            }

            Assert.Equal(2, counter);
        }

        [Fact]
        public void TestSelectWhenTrowingExceptions()
        {
            List<Employee> employees = null;


            Func<Employee, bool> myFunc = (x) => x.FirstName.StartsWith('P');
            var selectedEmployees = LINQFunctions.Select(employees, p => myFunc(p));

            Assert.Throws<ArgumentNullException>(() => selectedEmployees.Count());

        }

        [Fact]
        public void TestSelectMany()
        {
            var employees = Employee.GetEmployees();

            Func<Employee, List<Department>> myFunc = (x) => x.Departments;
            var selectedEmployees = LINQFunctions.SelectMany(employees, p => myFunc(p));

            Assert.Equal(12, selectedEmployees.Count());
        }

        [Fact]
        public void TestSelectManyWhenThrowingExceptions()
        {
            List<Employee> employees = null;

            Func<Employee, List<Department>> myFunc = (x) => x.Departments;
            var selectedEmployees = LINQFunctions.SelectMany(employees, p => myFunc(p));

            Assert.Throws<ArgumentNullException>(() => selectedEmployees.Count());

        }
        [Fact]
        public void TestWhere()
        {
            var employees = Employee.GetEmployees();
            Func<Employee, bool> myFunc = (x) => x.FirstName.StartsWith('P');

            var selectedEmployees = LINQFunctions.Where(employees, p => myFunc(p));

            Assert.Equal(2, selectedEmployees.Count());
        }

        [Fact]
        public void TestWhereWhenThrowingExceptions()
        {
            List<Employee> employees = null
                ;
            Func<Employee, bool> myFunc = (x) => x.FirstName.StartsWith('P');

            var selectedEmployees = LINQFunctions.Where(employees, p => myFunc(p));

            Assert.Throws<ArgumentNullException>(() => selectedEmployees.Count());
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
        public void TestAllThrowningExceptions()
        {
            int[] array = null;

            Func<int, bool> myFunc = (x) => { return x % 2 == 0; };

            Assert.Throws<ArgumentNullException>(() => LINQFunctions.All(array, p => myFunc(p)));

            array = new int[] { 2, 4, 6 };

            Assert.Throws<ArgumentNullException>(() => LINQFunctions.All(array, null));
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
        public void TestAnyThrowingExceptions()
        {
            string[] array = null;

            Func<string, bool> myFunc = (x) => { return x.Length == 5; };


            Assert.Throws<ArgumentNullException>(() => LINQFunctions.Any(array, p => myFunc(p)));
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

        [Fact]
        public void TestFirstWhenThrowingExceptions()
        {
            int[] array = null;

            Func<int, bool> myFunc = (x) => { return x == 4; };

            Assert.Throws<ArgumentNullException>(() => LINQFunctions.First(array, p => myFunc(p)));
        }
    }
}
