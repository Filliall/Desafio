using DebtManagement.Application.Dtos;
using MediatR;

namespace DebtManagement.Application.Queries;

public class GetDebtsQuery : IRequest<List<DebtSummaryDto>>
{
    public DateTime? ReferenceDate { get; set; } = DateTime.Today;
}

