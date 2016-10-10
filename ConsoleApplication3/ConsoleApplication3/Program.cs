using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication3
{
    class Program
    {
        static void Main(string[] args)
        {
            double e, k = 1, pi1 = 1,pi2=0, s = 0;
            e = double.Parse(Console.ReadLine());
            while (Math.Abs(pi1 - pi2) > e)
            {
                pi1 = pi2;
                s += 1 / ((4 * k - 2) * (4 * k - 1));
                pi2 = 8 * s + 2 * Math.Log(2);
                k++;
            }
            Console.WriteLine(pi2);
            Console.ReadLine();

        }
    }
}
