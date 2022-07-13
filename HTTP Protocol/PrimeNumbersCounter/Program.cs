using System;

namespace PrimeNumbersCounter
{
    class Program
    {
        static void Main(string[] args)
        {
            int n = 10000000;
            int count = 0;
            for (int i = 1; i < n; i++)
            {
                bool isPrime = true;
                for (int j = 2; j < Math.Sqrt(i); j++)
                {
                    if (i % j == 0)
                    {
                        isPrime = false;
                    }
                }
                if (isPrime)
                {
                    count++;
                }
            }

            Console.WriteLine(count);
        }
    }
}
