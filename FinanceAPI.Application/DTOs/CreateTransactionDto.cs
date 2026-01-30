using FinanceAPI.Domain.Enums;

namespace FinanceAPI.Application.DTOs;

public class CreateTransactionDto
{
    public Guid AccountId { get; set; }
    public TransactionType Type { get; set; }
    public decimal Amount { get; set; }
    public string? Description { get; set; }
}
