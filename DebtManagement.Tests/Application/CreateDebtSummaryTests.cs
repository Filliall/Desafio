using AutoMapper;
using DebtManagement.Application.Dtos;
using DebtManagement.Application.Mapping;
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
            1545.75m
        );

        // Act
        var dto = mapper.Map<DebtSummaryDto>(summary);

        // Assert
        Assert.Equal(summary.DebtNumber, dto.DebtNumber);
        Assert.Equal(summary.OriginalAmount, dto.OriginalAmount);
        Assert.Equal(summary.UpdatedAmount, dto.UpdatedAmount);
    }
}