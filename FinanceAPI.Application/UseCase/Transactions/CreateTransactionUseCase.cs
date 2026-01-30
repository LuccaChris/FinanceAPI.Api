using FinanceAPI.Application.DTOs;
using FinanceAPI.Application.Interfaces;
using FinanceAPI.Domain.Entities;
using FinanceAPI.Domain.Enums;

namespace FinanceAPI.Application.UseCases.Transactions;

public class CreateTransactionUseCase
{
    private readonly IAccountRepository _accountRepository;
    private readonly ITransactionRepository _transactionRepository;

    public CreateTransactionUseCase(
        IAccountRepository accountRepository,
        ITransactionRepository transactionRepository)
    {
        _accountRepository = accountRepository;
        _transactionRepository = transactionRepository;
    }

    public async Task ExecuteAsync(Guid userId, CreateTransactionDto dto)
    {
        // 1) buscar conta
        var account = await _accountRepository.GetByIdAsync(dto.AccountId);
        if (account is null)
            throw new Exception("Conta não encontrada");

        // 2) segurança: conta tem que ser do usuário logado
        if (account.UserId != userId)
            throw new Exception("Proibido");

        // 3) aplica regra no domínio (saldo)
        if (dto.Type == TransactionType.Deposit)
            account.Deposit(dto.Amount);
        else if (dto.Type == TransactionType.Withdraw)
            account.Withdraw(dto.Amount);
        else
            throw new Exception("Tipo de transação inválido");

        // 4) registra transação
        var transaction = new Transaction(dto.AccountId, dto.Type, dto.Amount, dto.Description);

        // 5) salva tudo
        await _transactionRepository.AddAsync(transaction);
        await _accountRepository.UpdateAsync(account);
    }
}
