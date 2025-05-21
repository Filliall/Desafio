using MediatR;

namespace DebtManagement.Application.Queries;

public class GetDebtsQuery : IRequest<List<DebtSummaryDto>>
{
    public DateTime? ReferenceDate { get; set; } = DateTime.Today;
}

public record DebtSummaryDto(
    string DebtNumber,
    string DebtorName,
    int InstallmentCount,
    decimal OriginalAmount,
    int DaysOverdue,
    decimal UpdatedAmount);