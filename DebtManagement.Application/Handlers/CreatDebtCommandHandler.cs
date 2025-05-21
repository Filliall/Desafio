using AutoMapper;
using DebtManagement.Application.Commands;
using DebtManagement.Application.Dtos;
using DebtManagement.Domain.Entities;
using DebtManagement.Domain.Interfaces;
using MediatR;

namespace DebtManagement.Application.Handlers;

public class CreateDebtCommandHandler : IRequestHandler<CreateDebtCommand, DebtDto>
{
    private readonly IDebtRepository _debtRepository;
    private readonly IMapper _mapper;

    public CreateDebtCommandHandler(IDebtRepository debtRepository, IMapper mapper)
    {
        _debtRepository = debtRepository;
        _mapper = mapper;
    }

    public async Task<DebtDto> Handle(CreateDebtCommand request, CancellationToken cancellationToken)
    {

        var installments = _mapper.Map<List<DebtInstallment>>(request.Installments);

        var debt = new Debt(
            request.DebtNumber,
            request.DebtorName,
            request.DebtorCPF,
            request.InterestRate,
            request.FineRate,
            installments);

       

        await _debtRepository.AddAsync(debt);
        await _debtRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

        return _mapper.Map<DebtDto>(debt);
    }

}