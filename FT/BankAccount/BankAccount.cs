﻿namespace BankAccount
{
    public class BankAccount : IWitdrawalable, IDepositable
    {
  
        public BankAccount(string firstName, string lastName)
        {
            this.Id = Guid.NewGuid().ToString();
            this.Balance = 0;
            this.FirstName = firstName;
            this.LastName = lastName;
        }
        public string Id { get; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public decimal Balance { get; private set; }

        public void Deposit(decimal amount)
        {
            this.Balance += amount;
        }

        public void Withdrawal(decimal amount)
        {
            if (this.Balance < amount)
            {
                throw new ArgumentException("You don`t have enough balance");
            }
            this.Balance -= amount;
        }
    }
}