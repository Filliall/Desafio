using DebtManagement.Domain.Entities;

public class DebtTests
{
    [Fact]
    public void CalculateSummary_ShouldReturnCorrectValues()
    {
        // Arrange
        var installments = new List<DebtInstallment>
        {
            new(1,1, new DateTime(2020, 7, 10), 100m),
            new(2,2, new DateTime(2020, 8, 10), 100m),
            new(3, 3, new DateTime(2020, 9, 10), 100m)
        };

        var debt = new Debt("101010", "Fulano", "12345678901", 1m, 2m, installments);
        var referenceDate = new DateTime(2020, 9, 21);

        // Act
        var summary = debt.CalculateSummary(referenceDate);

        // Assert
        Assert.Equal(300m, summary.OriginalAmount);
        Assert.Equal(73, summary.DaysOverdue);
        Assert.Equal(310.20m, summary.UpdatedAmount, 2);
        Assert.Equal(6m, summary.OriginalAmount);
        Assert.Equal(4.20m, summary.UpdatedAmount, 2);
    }
}
