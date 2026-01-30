using FinanceAPI.Application.Interfaces;
using FinanceAPI.Domain.Entities;
using FinanceAPI.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace FinanceAPI.Infrastructure.Repositories;

public class TransactionRepository : ITransactionRepository
{
    private readonly FinanceDbContext _context;

    public TransactionRepository(FinanceDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Transaction transaction)
    {
        _context.Transactions.Add(transaction);
        await _context.SaveChangesAsync();
    }

    public async Task<List<Transaction>> GetByAccountIdAsync(Guid accountId)
        => await _context.Transactions
            .Where(x => x.AccountId == accountId)
            .OrderByDescending(x => x.Date)
            .ToListAsync();
}
