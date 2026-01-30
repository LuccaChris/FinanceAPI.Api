using FinanceAPI.Application.DTOs;
using FinanceAPI.Application.Interfaces;

namespace FinanceAPI.Application.UseCases.Transactions;

public class GetAccountTransactionsUseCase
{
    private readonly IAccountRepository _accountRepository;
    private readonly ITransactionRepository _transactionRepository;

    public GetAccountTransactionsUseCase(
        IAccountRepository accountRepository,
        ITransactionRepository transactionRepository)
    {
        _accountRepository = accountRepository;
        _transactionRepository = transactionRepository;
    }

    public async Task<List<TransactionDto>> ExecuteAsync(Guid userId, Guid accountId)
    {
        var account = await _accountRepository.GetByIdAsync(accountId);
        if (account is null) throw new Exception("Conta não encontrada");
        if (account.UserId != userId) throw new Exception("Proibido");

        var list = await _transactionRepository.GetByAccountIdAsync(accountId);

        return list.Select(t => new TransactionDto
        {
            Id = t.Id,
            AccountId = t.AccountId,
            Type = t.Type,
            Amount = t.Amount,
            Description = t.Description,
            Date = t.Date
        }).ToList();
    }
}
