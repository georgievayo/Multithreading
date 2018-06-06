using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace CalculateE.v4
{
    class Program
    {
        static void Main(string[] args)
        {
            long precision = 0;
            int maxThreads = -1;
            string fileName = "output.txt";

            for (int i = 0; i < args.Length; i += 2)
            {
                if (args[i] == "-p")
                {
                    precision = long.Parse(args[i + 1]);
                }
                else if (args[i] == "-t")
                {
                    maxThreads = int.Parse(args[i + 1]);
                }
                else if (args[i] == "-o")
                {
                    fileName = args[i + 1];
                }
            }

            double sum = 0;
            var stopwatch = new Stopwatch();
            stopwatch.Start();


            Parallel.For(0, maxThreads, new ParallelOptions { MaxDegreeOfParallelism = maxThreads }, (threadIndex) =>
            {
                long start = threadIndex * ((precision + 1) / maxThreads);
                long end = start + (precision + 1) / maxThreads - 1;

                if (threadIndex == maxThreads - 1 && end < precision)
                {
                    end = precision;
                }
                Console.WriteLine($"Thread {threadIndex}, Start {start}, End {end}");


                long c = start;

                foreach (double value in GenerateFactorial1())
                {
                    if (c > end)
                    {
                        break;
                    }
                    double element = CalculateElement(c, value);
                    sum += element;
                    c++;
                }
            });

            stopwatch.Stop();
            Console.WriteLine(sum);

            using (StreamWriter writer = new StreamWriter(fileName))
            {
                writer.WriteLine("Sum is: " + sum);
                writer.WriteLine("Time elapsed: " + stopwatch.ElapsedMilliseconds + " milliseconds");
                writer.WriteLine("Time elapsed: " + stopwatch.ElapsedTicks + " ticks");
            }
        }

        static IEnumerable<double> GenerateFactorial1()
        {
            double product = 1;
            int n = 0;


            while (true)
            {
                if (n <= 1)
                {
                    n += 2;
                    yield return (double)1;
                }
                else
                {
                    product *= (double)n * (n - 1);
                    n += 2;
                    yield return (double)product;
                }
            }
        }

        static double CalculateElement(long k, double factorial)
        {
            return (2 * k + 1) / factorial;
        }
    }
}
