using AutoMapper;
using DebtManagement.Application.Commands;
using DebtManagement.Application.Dtos;
using DebtManagement.Application.Handlers;
using DebtManagement.Domain.Entities;
using DebtManagement.Domain.Interfaces;
using Moq;

public class CreateDebtCommandHandlerTests
{
    [Fact]
    public async Task Handle_ShouldAddDebtToRepository()
    {
        // Arrange
        var debtRepositoryMock = new Mock<IDebtRepository>();
        debtRepositoryMock.Setup(x => x.UnitOfWork.SaveEntitiesAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        var mapperMock = new Mock<IMapper>();
        mapperMock.Setup(x => x.Map<DebtDto>(It.IsAny<Debt>()))
            .Returns(new DebtDto());

        var handler = new CreateDebtCommandHandler(debtRepositoryMock.Object, mapperMock.Object);

        var command = new CreateDebtCommand(
            "101010",
            "Fulano",
            "12345678901",
            1m,
            2m,
            new List<DebtInstallmentDto>
            {
                new DebtInstallmentDto() { DueDate = DateTime.Today, Value = 100m }
            });

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        debtRepositoryMock.Verify(x => x.AddAsync(It.IsAny<Debt>()), Times.Once);
        debtRepositoryMock.Verify(x => x.UnitOfWork.SaveEntitiesAsync(It.IsAny<CancellationToken>()), Times.Once);
        Assert.NotNull(result);
    }
}