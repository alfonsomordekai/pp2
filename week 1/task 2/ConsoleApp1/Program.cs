using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task2
{
    class Student
    {
        public string name;
        public string id;
        public int year;

        public Student(string a, string b, int c)
        {
            name = a;
            id = b;
            year = c;
        }
        public void Print()
        {
            Console.WriteLine(name + " " + id + " " + year);
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            string a = Console.ReadLine();
            string b = Console.ReadLine();
            int c = int.Parse(Console.ReadLine());

            Student s = new Student(a, b, c);

            for (int i = 0; i < 4; i++)
            {
                s.Print();
                ++c;
            }
        }
    }
}