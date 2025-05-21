namespace DebtManagement.Application.Dtos;

public class DebtSummaryDto
{
    public string DebtNumber { get; set; }
    public string DebtorName { get; set; }
    public int InstallmentCount { get; set; }
    public decimal OriginalAmount { get; set; }
    public int DaysOverdue { get; set; }
    public decimal UpdatedAmount { get; set; }
    public decimal FineAmount { get; }
    public decimal InterestAmount { get; }
}
