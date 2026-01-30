using FinanceAPI.Domain.Entities;

namespace FinanceAPI.Application.Interfaces;

public interface ITransactionRepository
{
    Task AddAsync(Transaction transaction);
    Task<List<Transaction>> GetByAccountIdAsync(Guid accountId);
}
