using DebtManagement.Application.Dtos;
using MediatR;

namespace DebtManagement.Application.Commands;

public record CreateDebtCommand(
    string DebtNumber,
    string DebtorName,
    string DebtorCPF,
    decimal InterestRate,
    decimal FineRate,
    List<DebtInstallmentDto> Installments) : IRequest<DebtDto>;

