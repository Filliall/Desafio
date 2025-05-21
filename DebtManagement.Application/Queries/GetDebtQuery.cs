using DebtManagement.Domain.Entities;
using MediatR;

namespace DebtManagement.Application.Queries;

public class GetDebtQuery : IRequest<Debt>
{
    public string DebtNumber { get; set; }
}
