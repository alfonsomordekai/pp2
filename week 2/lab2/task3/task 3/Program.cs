using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task3
{
    class Program
    {
        static void Print_info(FileSystemInfo a, int b)
        {
            string s = new string(' ', b);
            s = s + a.Name;
            Console.WriteLine(s);

            if (a.GetType() == typeof(DirectoryInfo))
            {
                var c = (a as DirectoryInfo).GetFileSystemInfos();
                foreach (var n in c)
                {
                    Print_info(n, b + 3);
                }
            }
        }
        static void Main(string[] args)
        {
            DirectoryInfo a = new DirectoryInfo(@"C:\Users\Admin\Desktop\proga");

            Print_info(a, 1);

        }
    }
}