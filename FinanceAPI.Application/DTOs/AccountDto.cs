using FinanceAPI.Domain.Enums;

namespace FinanceAPI.Application.DTOs;

public class AccountDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public AccountType Type { get; set; }
    public decimal Balance { get; set; }
}
