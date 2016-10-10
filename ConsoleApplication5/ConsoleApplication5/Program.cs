using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication5
{
    class Program
    {
        static void Main(string[] args)
        {
            double a=2, b=3, s=0, n,x,i;
            n = double.Parse(Console.ReadLine());

            x = a;
            i= (b - a) / n;
            while (x<=b)
            {
                s += -Math.Sin(Math.Tan(x+i/2));
                x += i;
            }
            Console.WriteLine(s/n);
            Console.ReadLine();

        }
    }
}
