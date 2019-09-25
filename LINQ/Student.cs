using System;
using System.Collections.Generic;
using System.Text;

namespace LINQ
{
    public class Student
    {
        public Student(int id, string name)
        {
            this.Name = name;
            this.Id = id;
        }

        public int Id { get; set; }
        public string Name { get; set; }
    }
}
