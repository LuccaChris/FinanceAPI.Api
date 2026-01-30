using FinanceAPI.Application.DTOs;
using FinanceAPI.Application.Interfaces;
using FinanceAPI.Domain.Entities;

namespace FinanceAPI.Application.UseCases.Accounts;

public class CreateAccountUseCase
{
    private readonly IAccountRepository _accountRepository;

    public CreateAccountUseCase(IAccountRepository accountRepository)
    {
        _accountRepository = accountRepository;
    }

    public async Task ExecuteAsync(Guid userId, CreateAccountDto dto)
    {
        var account = new Account(userId, dto.Name, dto.Type);
        await _accountRepository.AddAsync(account);
    }
}
