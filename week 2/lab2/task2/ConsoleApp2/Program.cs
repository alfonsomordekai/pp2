using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task2
{
    class Program
    {
        static void Main(string[] args)
        {
            FileStream a = new FileStream(@"C:\Users\Admin\Desktop\proga\pp2\week 2\xxx.txt", FileMode.Open, FileAccess.Read);
            StreamReader b = new StreamReader(a);
            string l = b.ReadLine();
            a.Close();
            b.Close();
            bool bl=true;
            string c = "";
            string[] s = l.Split();
            foreach(var q in s)
            {
                long lp = Convert.ToInt64(q);
                if (lp == 1)
                {
                    bl = false;
                }
                for (int j = 2; j < lp-1; ++j)
                {
                    if (lp % j == 0)
                        bl = false;
                }
                if (bl)
                {
                    string tu = Convert.ToString(lp);
                    c = c + " " + tu;
                }
                bl = true;
            }
            FileStream d = new FileStream(@"C:\Users\Admin\Desktop\proga\pp2\week 2\xxxx.txt", FileMode.OpenOrCreate, FileAccess.Write);
            StreamWriter e= new StreamWriter(d);
            e.WriteLine(c);
            e.Close();
            d.Close();

        }
    }
}