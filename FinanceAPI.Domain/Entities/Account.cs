using FinanceAPI.Domain.Common;
using FinanceAPI.Domain.Enums;

namespace FinanceAPI.Domain.Entities
{
    public class Account : BaseEntity
    {
        public Guid UserId {get; private set; }
        public string Name { get; private set; } = default!;
        public AccountType Type { get; private set; }
        public decimal Balance { get; private set; }

        protected Account() { } // EF Core constructor

        public Account(Guid userId, string name, AccountType type)
        {
            UserId = userId;
            Name = name;
            Type = type;
            Balance = 0m;
        }

        public void Deposit(decimal amount)
        {
            if (amount <= 0)
                throw new ArgumentException("O valor do depósito deve ser positivo.", nameof(amount));

            Balance += amount;
            SetUpdated();
        }
        public void Withdraw(decimal amount)
        {
            if (amount <= 0)
                throw new ArgumentException("O valor do saque deve ser positivo.", nameof(amount));

            if (amount > Balance)
                throw new InvalidOperationException("Saldo insuficiente para o saque.");

            Balance -= amount;
            SetUpdated();
        }
    }
}