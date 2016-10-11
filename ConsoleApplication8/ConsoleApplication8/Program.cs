using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication36
{
    class Program
    {
        static void Main(string[] args)
        {
            
                int[] a = new int[50];
            int i = 0,sum = 0,k = 0,j = 0;
            Random z = new Random();
            while (i < 50)
            {
                a[i] = z.Next(-100, 100);
                i++;
            }


            i = 0;
            while (i < 50)
            {
                if (a[i] > 0)
                    sum += a[i];
                i++;
            }
            Console.WriteLine(sum);

            i = 0;
            while (i < 49)
            {
                if ((a[i] >= 0 && a[i + 1] < 0) || (a[i] < 0 && a[i + 1] >= 0))
                    k++;
                i++;
            }
            Console.WriteLine(k);

            k = 0;
            i = 0;
            while (i < 49)
            {
                if (a[i] >= a[i + 1])
                    k++;
                i++;
            }
            k = 0;
            i = 0;
            if (k == 49)
                Console.WriteLine("Yes");
            else
            {
                while (i < 49)
                {
                    if (a[i] <= a[i + 1])
                        k++;
                    i++;
                }
                if (k == 49)
                    Console.WriteLine("Yes");
                else
                    Console.WriteLine("No");
            }

            k = 0;
            i = 0;
            while (i < 49)
            {
                j = i + 1;
                while (j < 49)
                {
                    if (a[i] == a[j])
                    {

                        k++;
                    }
                    j++;

                }
                i++;
            }


            if (k > 1)
                Console.WriteLine($"Yes, {50 - k} ");
            else
                Console.WriteLine("No");

            Console.WriteLine(k);
            Console.ReadLine();

        }
    }
}
