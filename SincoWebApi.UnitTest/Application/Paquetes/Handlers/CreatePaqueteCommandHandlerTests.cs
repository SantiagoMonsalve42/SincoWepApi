using Moq;
using SincoWebApi.Application.Dtos;
using SincoWebApi.Application.Paquetes.Commands;
using SincoWebApi.Application.Paquetes.Handlers;
using SincoWebApi.Domain.Entities;
using SincoWebApi.Domain.Interfaces;
using SincoWebApi.Domain.Interfaces.Repositories;

namespace SincoWebApi.UnitTest.Application.Paquetes.Handlers;

public sealed class CreatePaqueteCommandHandlerTests
{
    [Fact]
    public async Task Handle_ShouldCreateAndReturnMappedResponse()
    {
        var paquetesRepoMock = new Mock<IPaqueteRepository>();
        var unitOfWorkMock = new Mock<IUnitOfWork>();
        var generatorMock = new Mock<ICodigoSeguimientoGenerator>();

        generatorMock.Setup(x => x.Generate()).Returns("TRACK-001");
        unitOfWorkMock.SetupGet(x => x.Paquetes).Returns(paquetesRepoMock.Object);
        unitOfWorkMock.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

        paquetesRepoMock
            .Setup(x => x.AddAsync(It.IsAny<Paquete>(), It.IsAny<CancellationToken>()))
            .Callback<Paquete, CancellationToken>((p, _) => p.PaqueteId = 100)
            .Returns(Task.CompletedTask);

        paquetesRepoMock
            .Setup(x => x.GetByIdWithDetailsAsync(100, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new Paquete
            {
                PaqueteId = 100,
                Descripcion = "Caja test",
                Peso = 3m,
                CodigoSeguimiento = "TRACK-001",
                PrioridadId = 1,
                EstadoId = 1,
                Prioridad = new Prioridad { PrioridadId = 1, Descripcion = "Alta" },
                EstadoPaquete = new EstadoPaquete { EstadoId = 1, Descripcion = "En bodega" }
            });

        var handler = new CreatePaqueteCommandHandler(unitOfWorkMock.Object, generatorMock.Object);
        var command = new CreatePaqueteCommand(new PaqueteCreateRequest
        {
            Descripcion = "Caja test",
            Peso = 3m,
            PrioridadId = 1
        });

        var result = await handler.Handle(command, CancellationToken.None);

        Assert.Equal(100, result.PaqueteId);
        Assert.Equal("TRACK-001", result.CodigoSeguimiento);
        Assert.True(result.EsPrioridadAlta);

        paquetesRepoMock.Verify(x => x.AddAsync(It.IsAny<Paquete>(), It.IsAny<CancellationToken>()), Times.Once);
        unitOfWorkMock.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }
}