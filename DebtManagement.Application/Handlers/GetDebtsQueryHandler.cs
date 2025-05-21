using AutoMapper;
using DebtManagement.Application.Dtos;
using DebtManagement.Application.Queries;
using DebtManagement.Domain.Interfaces;
using MediatR;

namespace DebtManagement.Application.Handlers;
public class GetDebtsQueryHandler : IRequestHandler<GetDebtsQuery, List<DebtSummaryDto>>
{
    private readonly IDebtRepository _debtRepository;
    private readonly IMapper _mapper;

    public GetDebtsQueryHandler(IDebtRepository debtRepository, IMapper mapper)
    {
        _debtRepository = debtRepository;
        _mapper = mapper;
    }

    public async Task<List<DebtSummaryDto>> Handle(GetDebtsQuery request, CancellationToken cancellationToken)
    {
        var debts = await _debtRepository.GetAllAsync();
        var referenceDate = request.ReferenceDate ?? DateTime.Today;

        return debts
            .Select(d => d.CalculateSummary(referenceDate))
            .Select(s => _mapper.Map<DebtSummaryDto>(s))
            .ToList();
    }
}
