using Moq;
using SincoWebApi.Application.Dtos;
using SincoWebApi.Application.Paquetes.Commands;
using SincoWebApi.Application.Paquetes.Handlers;
using SincoWebApi.Domain.Constants;
using SincoWebApi.Domain.Entities;
using SincoWebApi.Domain.Interfaces;
using SincoWebApi.Domain.Interfaces.Repositories;

namespace SincoWebApi.UnitTest.Application.Paquetes.Handlers;

public sealed class AssignRepartidorCommandHandlerTests
{
    [Fact]
    public async Task Handle_ShouldReturnFalse_WhenPaqueteDoesNotExist()
    {
        var paquetesRepoMock = new Mock<IPaqueteRepository>();
        var paqueteRepartidorRepoMock = new Mock<IPaqueteRepartidorRepository>();
        var unitOfWorkMock = new Mock<IUnitOfWork>();

        unitOfWorkMock.SetupGet(x => x.Paquetes).Returns(paquetesRepoMock.Object);
        unitOfWorkMock.SetupGet(x => x.PaqueteRepartidores).Returns(paqueteRepartidorRepoMock.Object);
        paquetesRepoMock.Setup(x => x.GetByIdAsync(10, It.IsAny<CancellationToken>())).ReturnsAsync((Paquete?)null);

        var handler = new AssignRepartidorCommandHandler(unitOfWorkMock.Object);
        var command = new AssignRepartidorCommand(10, new PaqueteAssignRequest { RepartidorId = 5 });

        var result = await handler.Handle(command, CancellationToken.None);

        Assert.False(result);
        unitOfWorkMock.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact]
    public async Task Handle_ShouldAssignAndSave_WhenPaqueteExists()
    {
        var paquete = new Paquete
        {
            PaqueteId = 10,
            EstadoId = EstadoPaqueteIds.EnBodega
        };

        var paquetesRepoMock = new Mock<IPaqueteRepository>();
        var paqueteRepartidorRepoMock = new Mock<IPaqueteRepartidorRepository>();
        var unitOfWorkMock = new Mock<IUnitOfWork>();

        unitOfWorkMock.SetupGet(x => x.Paquetes).Returns(paquetesRepoMock.Object);
        unitOfWorkMock.SetupGet(x => x.PaqueteRepartidores).Returns(paqueteRepartidorRepoMock.Object);

        paquetesRepoMock.Setup(x => x.GetByIdAsync(10, It.IsAny<CancellationToken>())).ReturnsAsync(paquete);
        paqueteRepartidorRepoMock.Setup(x => x.CountAsignadosPorRepartidorAsync(5, It.IsAny<CancellationToken>())).ReturnsAsync(1);
        paqueteRepartidorRepoMock.Setup(x => x.AddAsync(It.IsAny<PaqueteRepartidor>(), It.IsAny<CancellationToken>())).Returns(Task.CompletedTask);
        unitOfWorkMock.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

        var handler = new AssignRepartidorCommandHandler(unitOfWorkMock.Object);
        var command = new AssignRepartidorCommand(10, new PaqueteAssignRequest { RepartidorId = 5 });

        var result = await handler.Handle(command, CancellationToken.None);

        Assert.True(result);
        Assert.Equal(EstadoPaqueteIds.Asignado, paquete.EstadoId);

        paqueteRepartidorRepoMock.Verify(
            x => x.AddAsync(It.Is<PaqueteRepartidor>(a => a.PaqueteId == 10 && a.RepartidorId == 5), It.IsAny<CancellationToken>()),
            Times.Once);

        paquetesRepoMock.Verify(x => x.Update(paquete), Times.Once);
        unitOfWorkMock.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }
}