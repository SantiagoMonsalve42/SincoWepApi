using Moq;
using SincoWebApi.Application.Paquetes.Commands;
using SincoWebApi.Application.Paquetes.Handlers;
using SincoWebApi.Domain.Constants;
using SincoWebApi.Domain.Entities;
using SincoWebApi.Domain.Interfaces;
using SincoWebApi.Domain.Interfaces.Repositories;

namespace SincoWebApi.UnitTest.Application.Paquetes.Handlers;

public sealed class DeliverPaqueteCommandHandlerTests
{
    [Fact]
    public async Task Handle_ShouldReturnFalse_WhenPaqueteDoesNotExist()
    {
        var paquetesRepoMock = new Mock<IPaqueteRepository>();
        var unitOfWorkMock = new Mock<IUnitOfWork>();

        unitOfWorkMock.SetupGet(x => x.Paquetes).Returns(paquetesRepoMock.Object);
        paquetesRepoMock.Setup(x => x.GetByIdAsync(1, It.IsAny<CancellationToken>())).ReturnsAsync((Paquete?)null);

        var handler = new DeliverPaqueteCommandHandler(unitOfWorkMock.Object);

        var result = await handler.Handle(new DeliverPaqueteCommand(1), CancellationToken.None);

        Assert.False(result);
    }

    [Fact]
    public async Task Handle_ShouldMoveToEntregado_AndSave()
    {
        var paquete = new Paquete
        {
            PaqueteId = 1,
            EstadoId = EstadoPaqueteIds.Asignado
        };

        var paquetesRepoMock = new Mock<IPaqueteRepository>();
        var unitOfWorkMock = new Mock<IUnitOfWork>();

        unitOfWorkMock.SetupGet(x => x.Paquetes).Returns(paquetesRepoMock.Object);
        paquetesRepoMock.Setup(x => x.GetByIdAsync(1, It.IsAny<CancellationToken>())).ReturnsAsync(paquete);
        unitOfWorkMock.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

        var handler = new DeliverPaqueteCommandHandler(unitOfWorkMock.Object);

        var result = await handler.Handle(new DeliverPaqueteCommand(1), CancellationToken.None);

        Assert.True(result);
        Assert.Equal(EstadoPaqueteIds.Entregado, paquete.EstadoId);
        paquetesRepoMock.Verify(x => x.Update(paquete), Times.Once);
        unitOfWorkMock.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }
}