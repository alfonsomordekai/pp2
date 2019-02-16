using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1
{
    class Program
    {
        static void Main(string[] args)
        {
            FileStream a = new FileStream(@"C:\Users\Admin\Desktop\proga\pp2\week 2\lab2\zzz.txt", FileMode.Open, FileAccess.Read);
            StreamReader b = new StreamReader(a);
            string c = b.ReadLine();
            string d = new string(c.ToCharArray().Reverse().ToArray());
       
            if (d == c) Console.WriteLine("Yes");
            else Console.WriteLine("No");
        }
    }
}