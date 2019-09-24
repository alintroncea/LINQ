using System;
using System.Collections.Generic;
using System.Text;

namespace LINQ
{
    public class Pet
    {
        public string Name { get; set; }
        public Person Owner
        {
            get; set;
        }

    }
}