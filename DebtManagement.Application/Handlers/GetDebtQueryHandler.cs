using DebtManagement.Application.Queries;
using DebtManagement.Domain.Entities;
using DebtManagement.Domain.Interfaces;
using MediatR;

namespace DebtManagement.Application.Handlers;

public class GetDebtQueryHandler : IRequestHandler<GetDebtQuery, Debt>
{
    private readonly IDebtRepository _debtRepository;

    public GetDebtQueryHandler(IDebtRepository debtRepository)
    {
        _debtRepository = debtRepository;
    }

    public async Task<Debt> Handle(GetDebtQuery request, CancellationToken cancellationToken)
    {
        return await _debtRepository.GetByNumberAsync(request.DebtNumber);
    }
}
