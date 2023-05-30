namespace BankAccount
{
    internal class Program
    {
        static void Main(string[] args)
        {
            BankAccount account = new BankAccount("Ivan", "Ivanov");
            account.Deposit(234);
            Console.WriteLine(account.Balance);
           
            try
            {
                account.Withdrawal(33.33m);
                Console.WriteLine(account.Balance);
                account.Withdrawal(333.44m);
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }

            Console.WriteLine(account.Balance);
           
        }
    }
}