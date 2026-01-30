using FinanceAPI.Domain.Entities;

namespace FinanceAPI.Application.Interfaces;

public interface IAccountRepository
{
    Task AddAsync(Account account);
    Task<List<Account>> GetByUserIdAsync(Guid userId);
    Task<Account?> GetByIdAsync(Guid id);
}
