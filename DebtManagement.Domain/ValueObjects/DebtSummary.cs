

namespace DebtManagement.Domain.ValueObjects;
public class DebtSummary
{
    public string DebtNumber { get; }
    public string DebtorName { get; }
    public int InstallmentCount { get; }
    public decimal OriginalAmount { get; }
    public int DaysOverdue { get; }
    public decimal UpdatedAmount { get; }
    public decimal FineAmount { get; }
    public decimal InterestAmount { get; }

    public DebtSummary(
        string debtNumber,
        string debtorName,
        int installmentCount,
        decimal originalAmount,
        int daysOverdue,
        decimal updatedAmount, decimal fineAmount, decimal interestAmount)
    {
        DebtNumber = debtNumber;
        DebtorName = debtorName;
        InstallmentCount = installmentCount;
        OriginalAmount = originalAmount;
        DaysOverdue = daysOverdue;
        UpdatedAmount = updatedAmount;
        FineAmount = fineAmount;
        InterestAmount = interestAmount;
    }
}