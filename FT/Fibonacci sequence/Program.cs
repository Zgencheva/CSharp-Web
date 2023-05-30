namespace Fibonacci_sequence
{
    internal class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine("Please select a number:");
            int numbr = int.Parse(Console.ReadLine());
            FibonacciSequence(numbr);
        }

        public static void FibonacciSequence(int number)
        {
            if (number < 0)
            {
                throw new ArgumentException("Please select positive number.");
            }
            else if (number == 0)
            {
                Console.Write(0);
                return;
            }
            else
            {
                int a = 0;
                int b = 1;
                int c = 0;
                Console.Write($"{a} {b}");
                for (int i = 2; i < number; i++)
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