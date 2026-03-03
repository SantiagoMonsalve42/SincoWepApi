using Moq;
using SincoWebApi.Application.EstadosPaquete.Handlers;
using SincoWebApi.Application.EstadosPaquete.Queries;
using SincoWebApi.Domain.Entities;
using SincoWebApi.Domain.Interfaces;
using SincoWebApi.Domain.Interfaces.Repositories;

namespace SincoWebApi.UnitTest.Application.EstadosPaquete.Handlers;

public sealed class ListEstadosPaqueteQueryHandlerTests
{
    [Fact]
    public async Task Handle_ShouldReturnMappedEstados()
    {
        var repoMock = new Mock<IEstadoPaqueteRepository>();
        var unitOfWorkMock = new Mock<IUnitOfWork>();

        unitOfWorkMock.SetupGet(x => x.EstadosPaquete).Returns(repoMock.Object);
        repoMock.Setup(x => x.ListAsync(It.IsAny<CancellationToken>())).ReturnsAsync(new List<EstadoPaquete>
        {
            new() { EstadoId = 1, Descripcion = "En bodega" },
            new() { EstadoId = 2, Descripcion = "Asignado" }
        });

        var handler = new ListEstadosPaqueteQueryHandler(unitOfWorkMock.Object);

        var result = await handler.Handle(new ListEstadosPaqueteQuery(), CancellationToken.None);

        Assert.Equal(2, result.Count);
        Assert.Equal("En bodega", result[0].Descripcion);
    }
}