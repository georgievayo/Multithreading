using System;
using System.Diagnostics;

namespace CalculateE.v2
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

            Stopwatch sw = new Stopwatch();
            sw.Start();
            int lastNumber = 2 * precision;
            double[] factorials = new double[lastNumber + 1];
            factorials[0] = 1;
            factorials[1] = 1;

            for (int i = 2; i <= lastNumber; i++)
            {
                factorials[i] = factorials[i - 1] * i;
            }

            double sum = 0;
            for (int i = 0; i < precision; i++)
            {
                sum += (2 * i + 1) / factorials[2 * i];
            }

            sw.Stop();
            Console.WriteLine(sw.ElapsedMilliseconds);
            Console.WriteLine(sum);
        }
    }
}
