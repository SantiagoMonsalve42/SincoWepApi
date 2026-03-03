using Moq;
using SincoWebApi.Application.Paquetes.Handlers;
using SincoWebApi.Application.Paquetes.Queries;
using SincoWebApi.Domain.Entities;
using SincoWebApi.Domain.Interfaces;
using SincoWebApi.Domain.Interfaces.Repositories;

namespace SincoWebApi.UnitTest.Application.Paquetes.Handlers;

public sealed class GetPaqueteByIdQueryHandlerTests
{
    [Fact]
    public async Task Handle_ShouldReturnNull_WhenNotFound()
    {
        var paquetesRepoMock = new Mock<IPaqueteRepository>();
        var unitOfWorkMock = new Mock<IUnitOfWork>();

        unitOfWorkMock.SetupGet(x => x.Paquetes).Returns(paquetesRepoMock.Object);
        paquetesRepoMock.Setup(x => x.GetByIdWithDetailsAsync(5, It.IsAny<CancellationToken>())).ReturnsAsync((Paquete?)null);

        var handler = new GetPaqueteByIdQueryHandler(unitOfWorkMock.Object);

        var result = await handler.Handle(new GetPaqueteByIdQuery(5), CancellationToken.None);

        Assert.Null(result);
    }

    [Fact]
    public async Task Handle_ShouldReturnMappedResponse_WhenFound()
    {
        var paquetesRepoMock = new Mock<IPaqueteRepository>();
        var unitOfWorkMock = new Mock<IUnitOfWork>();

        unitOfWorkMock.SetupGet(x => x.Paquetes).Returns(paquetesRepoMock.Object);
        paquetesRepoMock
            .Setup(x => x.GetByIdWithDetailsAsync(5, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new Paquete
            {
                PaqueteId = 5,
                Descripcion = "Paquete",
                Peso = 1.2m,
                CodigoSeguimiento = "X1",
                PrioridadId = 1,
                EstadoId = 2,
                Prioridad = new Prioridad { Descripcion = "Alta" },
                EstadoPaquete = new EstadoPaquete { Descripcion = "Asignado" }
            });

        var handler = new GetPaqueteByIdQueryHandler(unitOfWorkMock.Object);

        var result = await handler.Handle(new GetPaqueteByIdQuery(5), CancellationToken.None);

        Assert.NotNull(result);
        Assert.Equal(5, result.PaqueteId);
        Assert.True(result.EsPrioridadAlta);
        Assert.Equal("Alta", result.Prioridad);
        Assert.Equal("Asignado", result.Estado);
    }
}