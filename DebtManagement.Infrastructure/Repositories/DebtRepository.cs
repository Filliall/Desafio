using DebtManagement.Domain.Entities;
using DebtManagement.Domain.Interfaces;
using DebtManagement.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace DebtManagement.Infrastructure.Repositories;

public class DebtRepository : IDebtRepository
{
    private readonly DebtContext _context;

    public DebtRepository(DebtContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public IUnitOfWork UnitOfWork => _context;

    public async Task AddAsync(Debt debt)
    {
        await _context.Debts.AddAsync(debt);
    }

    public async Task<Debt> GetByNumberAsync(string debtNumber)
    {
        return await _context.Debts
            .Include(d => d.Installments)
            .FirstOrDefaultAsync(d => d.DebtNumber == debtNumber);
    }

    public async Task<List<Debt>> GetAllAsync()
    {
        return await _context.Debts
            .Include(d => d.Installments)
            .ToListAsync();
    }
}