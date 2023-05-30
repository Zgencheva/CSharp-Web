namespace Fibonacci_sequence
{
    internal class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine("Please select a number:");
            int length = int.Parse(Console.ReadLine());
            FibonacciSequence(length);
        }

        public static void FibonacciSequence(int length)
        {
            if (length <= 0)
            {
                throw new ArgumentException("Please select positive number.");
            }
            else if (length == 1)
            {
                Console.WriteLine(0);
            }
            else
            {
                int a = 0;
                int b = 1;
                int c = 0;
                Console.Write($"{a} {b}");
                for (int i = 2; i < length; i++)
                {
                    c = a + b;
                    Console.Write($" {c}");
                    a = b;
                    b = c;
                }
            }
           
        }
    }
}