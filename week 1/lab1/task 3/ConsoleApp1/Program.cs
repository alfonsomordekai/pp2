using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task3_method
{

    class Program
    {       
        public static int[] dbl(int[] a)
            //create function which returning integers
        {
            int[] doub = new int[a.Length * 2];
            for (int i = 0; i < a.Length; i++)
            {
                int temp = a[i];
                doub[2 * i] = doub[2 * i + 1] = a[i];
            }
            return doub;
        }
        static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());
            //create integer
            string[] a = Console.ReadLine().Split();
            //create string and read it from console
            //split strings by namespaces
            int[] b = new int[n];
            //create array with size n
            for (int i = 0; i < n; i++)
            {
                b[i] = Convert.ToInt32(a[i]);
                //converting string to integers
            }
            int[] c = dbl(b);
            for(int i = 0; i < c.Count(); ++i)
            {
                Console.Write(c[i]);
                Console.Write(" ");
            }
            
        }
    }
}