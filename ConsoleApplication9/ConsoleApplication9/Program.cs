using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication9
{
    class Program
    {
        static void Main(string[] args)
        {
            int n = 10;
            int[] a = new int[n];
            int i = 1,j,f ;
            int[] b = new int[n];
            int[] c = new int[2*n];
            Random z = new Random();
            a[0] = 1;
            b[0] = 1;
            while (i < n)
            {
                a[i] = z.Next(a[i-1], i * 10);
                b[i] = z.Next(b[i-1], i * 10);
                i++;
            }

            i = 0;
            while (i <  n )
            {
                Console.Write("{0,3}", a[i]);
                i++;
            }
            Console.WriteLine();
            i = 0;
            while (i <  n )
            {
                Console.Write("{0,3}", b[i]);
                i++;
            }
            Console.WriteLine();
            i = 0;
            j = 0;
            f = 0;
            while (i <= 2 * n - 1)
            {
                if ((j <= n - 1) && (f <= n - 1))
                {
                    if (a[j] > b[f])
                    {
                        c[i] = b[f];
                        f++;
                        i++;
                    }
                    else
                    {
                        if (a[j] < b[f])
                        {
                            c[i] = a[j];
                            j++;
                            i++;
                        }
                        else
                        {
                            c[i] = a[j];
                            c[i+1] = b[f];
                            j++;
                            f++;
                            i +=2;
                        }
                    }
                }
                else
                {
                    if (j == n)
                    {
                        c[i] = b[f];
                        f++;
                        i++;
                    }
                    else
                    {
                        c[i] = a[j];
                        j++;
                        i++;
                    }
                }

            }
            i = 0;
            while (i<2*n)
            {
                Console.Write("{0,3}", c[i]);
                i++;
            }

            Console.ReadLine();
        }
    }
}
