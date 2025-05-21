

namespace DebtManagement.Domain.Entities;

public class DebtInstallment
{
    public int DebtId { get; private set; } // Chave estrangeira
    public int InstallmentNumber { get; private set; }
    public DateTime DueDate { get; private set; }
    public decimal OriginalValue { get; private set; }

    private DebtInstallment() { } // Construtor privado para EF

    public DebtInstallment(
        int debtId,
        int installmentNumber,
        DateTime dueDate,
        decimal originalValue)
    {
        DebtId = debtId;
        InstallmentNumber = installmentNumber;
        DueDate = dueDate;
        OriginalValue = originalValue;
    }

    internal void SetDebtId(int debtId)
    {
        DebtId = debtId;
    }
    public int CalculateDaysOverdue(DateTime referenceDate)
    {
        if (DueDate >= referenceDate) return 0;
        return (int)(referenceDate - DueDate).TotalDays;
    }

    public decimal CalculateInterest(DateTime referenceDate, decimal monthlyInterestRate)
    {
        var daysOverdue = CalculateDaysOverdue(referenceDate);
        if (daysOverdue <= 0) return 0;

        var dailyInterestRate = monthlyInterestRate / 30 / 100;
        return OriginalValue * dailyInterestRate * daysOverdue;
    }


}
