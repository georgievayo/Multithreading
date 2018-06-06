using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace CalculateE.v3
{
    class Program
    {
        static void Main(string[] args)
        {
            int precision = -1;
            int maxThreads = -1;
            string fileName = "output.txt";

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

            for (int k = 0; k <= precision; k++)
            {
                double factorial = 1;
                Parallel.For(0, maxThreads, new ParallelOptions { MaxDegreeOfParallelism = maxThreads }, (threadIndex) =>
                {
                    int start = threadIndex * ((2 * k + 1) / maxThreads);
                    int end = start + (2 * k + 1) / maxThreads - 1;
                    if(threadIndex == maxThreads - 1 && end < 2 * k)
                    {
                        end = 2 * k;
                    }

                    if (end < start)
                    {
                        return;
                    }

                    var partialProduct = CalculateRangeProduct(start, end);

                    factorial *= partialProduct;

                });

                var element = CalculateElement(k, factorial);
                sum += element;
            }

            stopwatch.Stop();

            using (StreamWriter writer = new StreamWriter(fileName))
            {
                writer.WriteLine("Sum is: " + sum);
                writer.WriteLine("Time elapsed: " + stopwatch.ElapsedMilliseconds + " milliseconds");
            }
        }

        static double CalculateRangeProduct(int start, int end)
        {
            double product = 1;

            if(start == 0)
            {
                start++;
            }

            for (int i = start; i <= end; i++)
            {
                product *= i;
            }

            return product;
        }

        static double CalculateElement(int k, double factorial)
        {
            return (2 * k + 1) / factorial;
        }
    }
}
