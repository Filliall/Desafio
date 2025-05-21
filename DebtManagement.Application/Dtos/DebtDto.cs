namespace DebtManagement.Application.Dtos;

public class DebtDto
{

    public string DebtNumber { get; set; }
    public string DebtorName { get; set; }
    public string DebtorCPF { get; set; }
    public decimal InterestRate { get; set; }
    public decimal FineRate { get; set; }
    public List<DebtInstallmentDto> Installments { get; set; } = new();



}
