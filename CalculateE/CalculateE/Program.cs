using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace CalculateE
{
    class Program
    {
        static void Main(string[] args)
        {
            int precision = -1;
            int maxThreads = -1;
            string fileName = null;

            for (int i = 0; i < args.Length; i += 2)
            {
                if (args[i] == "-p")
                {
                    precision = int.Parse(args[i + 1]);
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

            Parallel.For(0, precision, new ParallelOptions { MaxDegreeOfParallelism = maxThreads }, (k) =>
            {
                var i = k;
                var element = CalculateElement(i);
                sum += element;
            });

            stopwatch.Stop();

            using (StreamWriter writer = new StreamWriter(fileName))
            {
                writer.WriteLine("Sum is: " + sum);
                writer.WriteLine("Time elapsed: " + stopwatch.ElapsedMilliseconds + " milliseconds");
            } 
        }

        static double CalculateElement(int k)
        {
            double factorial = 1;

            for (int i = 2; i <= 2 * k; i++)
            {
                factorial *= i;
            }

            return (2 * k + 1) / factorial;
        }
    }
}
