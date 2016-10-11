using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication13
{
    class Program
    {
        static void Main(string[] args)
        {
            double x, e, a1 = 0, a2, b, c, d;
            e = double.Parse(Console.ReadLine());
            x = double.Parse(Console.ReadLine());

            b = -1;
            c = x * x * x;
            d = 3;
            a2 = b * c / d;
            while (Math.Abs(a1 - a2) > e)
            {
                a1 += b * c / d;
                b *= -1;
                c *= x * x;
                d += 2;
                a2 += b * c / d;
            }
            Console.WriteLine(a2);
            Console.ReadLine();
        }
    }
}
