using System;
using System.Collections.Generic;
using System.Text;

namespace LINQ
{
    public class Student
    {
        public Student(int id, string name, string subject)
        {
            Name = name;
            ID = id;
            Class = subject;
        }

        public int ID { get; set; }
        public string Name { get; set; }
        public string Class { get; set; }


        public static List<Student> GetSudents()
        {
            var list = new List<Student>();
            list.Add(new Student(4, "Andrei", "Computer Science"));
            list.Add(new Student(2, "Mihai", "Biology"));

            list.Add(new Student(1, "Maria", "Computer Science"));
            list.Add(new Student(3, "Ana-Maria", "Biology"));
            list.Add(new Student(3, "Ioana", "Biology"));
            return list;
        }
    }
}