using AutoMapper;
using DebtManagement.Application.Commands;
using DebtManagement.Application.Dtos;
using DebtManagement.Application.Mapping;
using DebtManagement.Domain.Entities;
using DebtManagement.Domain.ValueObjects;

public class CreateDebtSummaryTests
{
    [Fact]
    public void DebtSummary_To_DebtSummaryDto_MapsCorrectly()
    {
        // Arrange
        var config = new MapperConfiguration(cfg => cfg.AddProfile<DebtProfile>());
        var mapper = config.CreateMapper();

        var summary = new DebtSummary(
            "2024-001",
            "João Silva",
            3,
            1500m,
            15,
            1545.75m,
            45.75m,
            0m
        );

        // Act
        var dto = mapper.Map<DebtSummaryDto>(summary);

        // Assert
        Assert.Equal(summary.DebtNumber, dto.DebtNumber);
        Assert.Equal(summary.OriginalAmount, dto.OriginalAmount);
        Assert.Equal(summary.UpdatedAmount, dto.UpdatedAmount);
    }

    [Fact]
    public void DebtInstallment_To_Dto_MapsValueCorrectly()
    {
        // Arrange
        var config = new MapperConfiguration(cfg => cfg.AddProfile<DebtProfile>());
        var mapper = config.CreateMapper();

        var entity = new DebtInstallment(
            debtId: 1,
            installmentNumber: 1,
            dueDate: DateTime.Today,
            originalValue: 100.50m
        );

        // Act
        var dto = mapper.Map<DebtInstallmentDto>(entity);

        // Assert
        Assert.Equal(entity.OriginalValue, dto.Value);
    }

    [Fact]
    public void CreateCommand_To_Debt_IgnoresId()
    {
        // Arrange
        var config = new MapperConfiguration(cfg => cfg.AddProfile<DebtProfile>());
        var mapper = config.CreateMapper();

        var command = new CreateDebtCommand(
            debtNumber: "2024-001",
            debtorName: "Test",
            debtorCPF: "123.456.789-09",
            interestRate: 1.5m,
            fineRate: 2.0m,
            installments: new List<DebtInstallmentDto>()
        );

        // Act
        var entity = mapper.Map<Debt>(command);

        // Assert
        Assert.Equal(0, entity.Id); // Id deve ser ignorado
        Assert.Equal(command.DebtNumber, entity.DebtNumber);
    }
}