using FinanceAPI.Application.Interfaces;
using FinanceAPI.Domain.Entities;
using FinanceAPI.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace FinanceAPI.Infrastructure.Repositories;

public class AccountRepository : IAccountRepository
{
    private readonly FinanceDbContext _context;

    public AccountRepository(FinanceDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Account account)
    {
        _context.Accounts.Add(account);
        await _context.SaveChangesAsync();
    }

    public async Task<List<Account>> GetByUserIdAsync(Guid userId)
        => await _context.Accounts
            .Where(x => x.UserId == userId)
            .OrderBy(x => x.Name)
            .ToListAsync();

    public async Task<Account?> GetByIdAsync(Guid id)
        => await _context.Accounts.FirstOrDefaultAsync(x => x.Id == id);
}
