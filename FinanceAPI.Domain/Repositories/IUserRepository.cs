using FinanceAPI.Domain.Entities;

namespace FinanceAPI.Domain.Repositories;

public interface IUserRepository
{
    Task<User?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task UpdateAsync(User user, CancellationToken ct = default);
}