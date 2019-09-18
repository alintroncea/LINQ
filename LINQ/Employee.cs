using System;
using System.Collections.Generic;
using System.Text;

namespace LINQ
{
    public class Employee
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Salary { get; set; }
        public List<Department> Departments { get; set; }

        public Employee()
        {
            Departments = new List<Department>();
        }

        public static List<Employee> GetEmployees()
        {
            List<Employee> employees = new List<Employee>
            {
                new Employee {ID = 104,
                    FirstName = "Anurag",
                    LastName = "Mohanty",
                    Salary = 90000,
                    Departments = { new Department { Name = "Marketing" }, new Department {Name = "Sales" } } },

                new Employee {ID = 105,
                    FirstName = "Sambit",
                    LastName = "Satapathy",
                    Salary = 100000,
                    Departments ={ new Department { Name = "Advertisement" }, new Department {Name = "Production" } } },

                new Employee { ID = 106,
                    FirstName = "Sushanta",
                    LastName = "Jena",
                    Salary = 160000,
                    Departments = { new Department { Name = "Production" }, new Department {Name = "Sales" } } },

                new Employee { ID = 101,
                    FirstName = "Preety",
                    LastName = "Tiwary",
                    Salary = 60000,
                    Departments = { new Department { Name = "Marketing" }, new Department {Name = "HR" } } },

                new Employee { ID = 103,
                    FirstName = "Hina",
                    LastName = "Sharma",
                    Salary = 80000,
                    Departments = { new Department { Name = "Advertisement" }, new Department {Name = "HR" } } },


                new Employee { ID = 102,
                    FirstName = "Priyanka",
                    LastName = "Dewangan",
                    Salary = 70000,
                    Departments = { new Department { Name = "Production" }, new Department {Name = "HR" } }
                }


            };
            return employees;
        }
    }

}
