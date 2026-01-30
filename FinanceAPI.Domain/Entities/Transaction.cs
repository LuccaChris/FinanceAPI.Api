using FinanceAPI.Domain.Common;
using FinanceAPI.Domain.Enums;

namespace FinanceAPI.Domain.Entities;

public class Transaction : BaseEntity
{
    public Guid AccountId { get; private set; }
    public TransactionType Type { get; private set; }
    public decimal Amount { get; private set; }
    public string? Description { get; private set; }
    public DateTime Date { get; private set; }

    protected Transaction() { } // EF

    public Transaction(Guid accountId, TransactionType type, decimal amount, string? description = null)
    {
        if (amount <= 0) throw new ArgumentException("O valor deve ser maior que zero.", nameof(amount));

        AccountId = accountId;
        Type = type;
        Amount = amount;
        Description = description;
        Date = DateTime.UtcNow;
    }
}
