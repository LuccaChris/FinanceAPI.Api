using FinanceAPI.Domain.Enums;

namespace FinanceAPI.Application.DTOs;

public class CreateAccountDto
{
    public string Name { get; set; } = default!;
    public AccountType Type { get; set; }
}
