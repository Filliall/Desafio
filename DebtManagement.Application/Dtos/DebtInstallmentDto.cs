namespace DebtManagement.Application.Dtos;

public class DebtInstallmentDto
{

    public int InstallmentNumber { get; set; }
    public DateTime DueDate { get; set; }
    public decimal Value { get; set; }
    public int V1 { get; }
    public DateTime Today { get; }
    public decimal V2 { get; }
}
