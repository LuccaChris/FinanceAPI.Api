using FinanceAPI.Application.DTOs;
using FinanceAPI.Application.Interfaces;

namespace FinanceAPI.Application.UseCases.Accounts;

public class GetMyAccountsUseCase
{
    private readonly IAccountRepository _accountRepository;

    public GetMyAccountsUseCase(IAccountRepository accountRepository)
    {
        _accountRepository = accountRepository;
    }

    public async Task<List<AccountDto>> ExecuteAsync(Guid userId)
    {
        var accounts = await _accountRepository.GetByUserIdAsync(userId);

        return accounts.Select(a => new AccountDto
        {
            Id = a.Id,
            Name = a.Name,
            Type = a.Type,
            Balance = a.Balance
        }).ToList();
    }
}
