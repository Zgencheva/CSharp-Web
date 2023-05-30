namespace Palindrome_check
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string input = Console.ReadLine();
            Console.WriteLine(IsPalindrome(input));
        }

        private static bool IsPalindrome(string input)
        {
            char[] inputAsArray = input.ToCharArray();
            Array.Reverse(inputAsArray);
            var result = new string(inputAsArray);
            if (result == input)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
    }
}