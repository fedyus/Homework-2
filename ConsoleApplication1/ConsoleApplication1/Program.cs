using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication38
{
    class Program
    {
        static void Main(string[] args)
        {
            double e, a1 = 0, a2, b = 1, c = 2, m = 1;
            e = double.Parse(Console.ReadLine());

            a2 = 0.5;
            while (Math.Abs(a1 - a2) > e)
            {
                m++;
                a1 = a2;
                b *= m - 1;
                c *= 2 * m * (m + 1);
                a2 += b * b / c;
            }

            Console.WriteLine(a2);
            Console.ReadLine();

        }
    }
}
