using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task4
{
    class Program
    {
        static void Main(string[] args)
        {
            FileStream a = new FileStream(@"C:\Users\Admin\Desktop\proga\pp2\week 2\yyyy.txt", FileMode.OpenOrCreate, FileAccess.ReadWrite);
            StreamWriter b = new StreamWriter(a);
            string s = "some information";
            b.WriteLine(s);
            b.Close();
            a.Close();
            File.Copy(@"C:\Users\Admin\Desktop\proga\pp2\week 2\yyyy.txt", @"C:\Users\Admin\Desktop\proga\pp2\week 2\yyy\yyyy.txt");
            File.Delete(@"C:\Users\Admin\Desktop\proga\pp2\week 2\yyyy.txt");

        }
    }
}