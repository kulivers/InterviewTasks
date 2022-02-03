using System;
using System.Collections.Generic;
using System.Linq;

namespace TestProject
{
    class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var hashset = new HashSet<Person>()
            {
                new Person() { Age = 11, Name = "1" },
                new Person() { Age = 11, Name = "2" },
                new Person() { Age = 11, Name = "3" },
                new Person() { Age = 11, Name = "4" },
                new Person() { Age = 11, Name = "5" }, 
                new Person() { Age = 12, Name = "6" },
                new Person() { Age = 12, Name = "7" },
            };
            var delCount = 3;
            var firstNPersons = hashset.Where(p => p.Age == 11).Take(delCount).ToArray();
            
            hashset.RemoveWhere(person => person.Age == 11);
            foreach (var person in firstNPersons)
            {
                hashset.Add(person);
            }

            var subSet = hashset.Where(p => p.Age > 11);
            foreach (var person in subSet)
            {
                person.Age = 2312;
            }
            Console.WriteLine("Hello World!");
        }
    }
}