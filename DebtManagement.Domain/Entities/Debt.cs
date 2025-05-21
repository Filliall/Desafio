

using DebtManagement.Domain.Exceptions;
using DebtManagement.Domain.ValueObjects;

namespace DebtManagement.Domain.Entities;

public class Debt
{
    public int Id { get; private set; }
    public string DebtNumber { get; private set; }
    public string DebtorName { get; private set; }
    public string DebtorCPF { get; private set; }
    public decimal InterestRate { get; private set; } // % ao mês
    public decimal FineRate { get; private set; }    // % sobre o valor original
    private readonly List<DebtInstallment> _installments = new();
    public IReadOnlyCollection<DebtInstallment> Installments => _installments.AsReadOnly();

    protected Debt() { }

    public Debt(string debtNumber, string debtorName, string debtorCPF,
        decimal interestRate, decimal fineRate, List<DebtInstallment> installments)
    {
        ValidateDebtNumber(debtNumber);
        ValidateDebtorName(debtorName);
        ValidateCPF(debtorCPF);
        ValidateRates(interestRate, fineRate);
        ValidateInstallments(installments);

        DebtNumber = debtNumber.Trim().ToUpper();
        DebtorName = debtorName.Trim();
        DebtorCPF = FormatCPF(debtorCPF);
        InterestRate = interestRate;
        FineRate = fineRate;
        _installments = installments;
    }

    public void AddInstallment(DebtInstallment installment)
    {
        if (Id == 0)
            throw new DomainException("Debt must be saved before adding installments");

        installment.SetDebtId(Id);
        _installments.Add(installment);
    }

    private void ValidateDebtNumber(string debtNumber)
    {
        if (string.IsNullOrWhiteSpace(debtNumber))
            throw new DomainException("Número do título é obrigatório");

        if (debtNumber.Length > 20)
            throw new DomainException("Número do título deve ter no máximo 20 caracteres");
    }

    private void ValidateDebtorName(string debtorName)
    {
        if (string.IsNullOrWhiteSpace(debtorName))
            throw new DomainException("Nome do devedor é obrigatório");

        if (debtorName.Length > 100)
            throw new DomainException("Nome do devedor deve ter no máximo 100 caracteres");
    }

    private void ValidateCPF(string cpf)
    {
        if (string.IsNullOrWhiteSpace(cpf))
            throw new DomainException("CPF é obrigatório");

        var cleanCpf = cpf.Trim().Replace(".", "").Replace("-", "");

        if (cleanCpf.Length != 11 || !long.TryParse(cleanCpf, out _))
            throw new DomainException("CPF inválido");

        if (!IsValidCpf(cleanCpf))
            throw new DomainException("CPF inválido");
    }

    private bool IsValidCpf(string cpf)
    {
        // Implementação da validação de CPF
        int[] multiplier1 = { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
        int[] multiplier2 = { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

        string tempCpf = cpf.Substring(0, 9);
        int sum = 0;

        for (int i = 0; i < 9; i++)
            sum += int.Parse(tempCpf[i].ToString()) * multiplier1[i];

        int remainder = sum % 11;
        remainder = remainder < 2 ? 0 : 11 - remainder;

        string digit = remainder.ToString();
        tempCpf += digit;

        sum = 0;
        for (int i = 0; i < 10; i++)
            sum += int.Parse(tempCpf[i].ToString()) * multiplier2[i];

        remainder = sum % 11;
        remainder = remainder < 2 ? 0 : 11 - remainder;

        digit += remainder.ToString();

        return cpf.EndsWith(digit);
    }

    private string FormatCPF(string cpf)
    {
        return Convert.ToUInt64(cpf).ToString(@"000\.000\.000\-00");
    }

    private void ValidateRates(decimal interestRate, decimal fineRate)
    {
        if (interestRate < 0 || interestRate > 100)
            throw new DomainException("Juros deve ser entre 0% e 100%");

        if (fineRate < 0 || fineRate > 100)
            throw new DomainException("Multa deve ser entre 0% e 100%");
    }

    private void ValidateInstallments(List<DebtInstallment> installments)
    {
        if (installments == null || !installments.Any())
            throw new DomainException("Deve haver pelo menos uma parcela");

        if (installments.Any(i => i.DueDate < DateTime.Today))
            throw new DomainException("Não são permitidas parcelas com data retroativa");

        var duplicateNumbers = installments
            .GroupBy(i => i.InstallmentNumber)
            .Any(g => g.Count() > 1);

        if (duplicateNumbers)
            throw new DomainException("Números de parcela não podem ser repetidos");
    }


    public DebtSummary CalculateSummary(DateTime referenceDate)
    {
        var originalAmount = Installments.Sum(i => i.OriginalValue);
        var fineAmount = originalAmount * (FineRate / 100);
        var interestAmount = CalculateTotalInterest(referenceDate);
        var updatedAmount = originalAmount + fineAmount + interestAmount;
        var maxDaysOverdue = Installments.Max(i => i.CalculateDaysOverdue(referenceDate));

        return new DebtSummary(
            DebtNumber,
            DebtorName,
            Installments.Count,
            originalAmount,
            maxDaysOverdue,
            updatedAmount,
            fineAmount,
            interestAmount);
    }

    private decimal CalculateTotalInterest(DateTime referenceDate)
    {
        return Installments.Sum(i => i.CalculateInterest(referenceDate, InterestRate));
    }
}