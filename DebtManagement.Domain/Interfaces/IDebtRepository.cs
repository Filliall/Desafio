using DebtManagement.Domain.Entities;

namespace DebtManagement.Domain.Interfaces;

public interface IDebtRepository
{
    Task AddAsync(Debt debt);
    Task<Debt> GetByNumberAsync(string debtNumber);
    Task<List<Debt>> GetAllAsync();
    IUnitOfWork UnitOfWork { get; }
}
