using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            string a;
            a = Console.ReadLine();
            int b;
            b = Convert.ToInt32(a);
            for (int i = 1; i <= b; ++i)
            {
                for(int j = 0; j < i; ++j)
                {
                    Console.Write   ("[*]");

                }
                Console.WriteLine();
            }

        }
    }
}
