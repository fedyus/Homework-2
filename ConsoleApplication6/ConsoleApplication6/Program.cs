using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication6
{
    class Program
    {
        static void Main(string[] args)
        {
            double a=0.5, b=2.5, s;

            s =0.559* (b - a) * (-Math.Tan(1 / a + a) - 4 * Math.Tan(2 / (a + b) + (a + b) / 2) - Math.Tan(1 / b + b)) / 3;
            Console.WriteLine(s);
            Console.ReadLine();
        }
    }
}
