using Moq;
using SincoWebApi.Application.Paquetes.Handlers;
using SincoWebApi.Application.Paquetes.Queries;
using SincoWebApi.Domain.Entities;
using SincoWebApi.Domain.Interfaces;
using SincoWebApi.Domain.Interfaces.Repositories;

namespace SincoWebApi.UnitTest.Application.Paquetes.Handlers;

public sealed class GetPaquetesQueryHandlerTests
{
    [Fact]
    public async Task Handle_ShouldReturnMappedList()
    {
        var paquetesRepoMock = new Mock<IPaqueteRepository>();
        var unitOfWorkMock = new Mock<IUnitOfWork>();

        unitOfWorkMock.SetupGet(x => x.Paquetes).Returns(paquetesRepoMock.Object);
        paquetesRepoMock
            .Setup(x => x.ListByEstadoAsync(1, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new List<Paquete>
            {
                new()
                {
                    PaqueteId = 1,
                    Descripcion = "A",
                    Peso = 1m,
                    CodigoSeguimiento = "A1",
                    PrioridadId = 1,
                    EstadoId = 1,
                    Prioridad = new Prioridad { Descripcion = "Alta" },
                    EstadoPaquete = new EstadoPaquete { Descripcion = "En bodega" }
                },
                new()
                {
                    PaqueteId = 2,
                    Descripcion = "B",
                    Peso = 2m,
                    CodigoSeguimiento = "B1",
                    PrioridadId = 2,
                    EstadoId = 1
                }
            });

        var handler = new GetPaquetesQueryHandler(unitOfWorkMock.Object);

        var result = await handler.Handle(new GetPaquetesQuery(1), CancellationToken.None);

        Assert.Equal(2, result.Count);
        Assert.True(result[0].EsPrioridadAlta);
        Assert.False(result[1].EsPrioridadAlta);
    }
}