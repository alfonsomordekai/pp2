using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace T1
{
    class Program
    {
        public static bool P(int a)
            //create public function prime
        {
            
            if (a == 1)
                return false;
            //checking a 
            for (int i = 2; i <= Math.Sqrt(a); i++)
            {
                if (a % i == 0)
                    return false;
                //return false if a was divided by any i
            }
            return true;
            //return true if a is prime
        }

        static void Main(string[] args)
        {
            int n;
            //create integer
            int cnt = 0;
            //create counter
            n = Convert.ToInt32(Console.ReadLine());
            // convert string from console to integer
            int[] b = new int[n];
            //create array of integer with size n;
            int[] c = new int[5050];
            //create array of integers 
            string[] s = new string[100];
            //create array of strings
            s = Console.ReadLine().Split();
            //read the string from console and split string by "namespace"
            for (int i = 0; i < n; i++)
            {
                b[i] = Convert.ToInt32(s[i]);
                //converting string to integer
                if (P(b[i]))
                 //call our function
                {
                    cnt++;
                    //counting how many primes there are
                }
            }
            Console.WriteLine(cnt);
            //write how many we have primes
            for (int i = 0; i < b.Count(); i++)
            {
                if (P(b[i]))
                //call our function
                {
                    Console.Write(b[i] + " ");
                    //write our primes
                }
            }

        }
    }
}