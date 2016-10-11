using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication11
{
    class Program
    {
        static void Main(string[] args)
        {
            double x, e, a1 = 1, a2 = 1, b, c, d, g, k = 1;
            e = double.Parse(Console.ReadLine());
            x = double.Parse(Console.ReadLine());

            b = -1;
            c = x;
            d = 3;
            g = 2;
            a2 += b * c * d / g;
            while (Math.Abs(a1 - a2) > e)
            {
                a1 += b * c * d / g;
                k++;
                b *= -1;
                c *= x;
                d *= (2 * k + 1);
                g *= 2 * k;
                a2 += b * c * d / g;
                Console.WriteLine(a2);
            }

            Console.WriteLine(1 / Math.Pow(1 + x, 0.5));
            Console.WriteLine(a2);
            Console.ReadLine();
        }
    }
}
