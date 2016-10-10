using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication28
{
    class Program
    {
        static void Main(string[] args)
        {
            double y, y1 = 0,y2=0, x, e;
            e = double.Parse(Console.ReadLine());
            x = double.Parse(Console.ReadLine());
            if (x >= 1)
                y2 = x / 3;
            else
                y2 = x;
            while (Math.Abs(y1 - y2) >= e)
            {
                y1 = y2;
                y2 = y1 - ((y1 * y1 * y1 - x) / (y1 * y1)) / 3;

            }
            Console.WriteLine(y2);
            Console.ReadLine();
        }
    }
}
