using Moq;
using SincoWebApi.Application.Repartidores.Handlers;
using SincoWebApi.Application.Repartidores.Queries;
using SincoWebApi.Domain.Entities;
using SincoWebApi.Domain.Interfaces;
using SincoWebApi.Domain.Interfaces.Repositories;

namespace SincoWebApi.UnitTest.Application.Repartidores.Handlers;

public sealed class ListRepartidoresQueryHandlerTests
{
    [Fact]
    public async Task Handle_ShouldReturnMappedRepartidores()
    {
        var repoMock = new Mock<IRepartidorRepository>();
        var unitOfWorkMock = new Mock<IUnitOfWork>();

        unitOfWorkMock.SetupGet(x => x.Repartidores).Returns(repoMock.Object);
        repoMock.Setup(x => x.ListAsync(It.IsAny<CancellationToken>())).ReturnsAsync(new List<Repartidor>
        {
            new() { RepartidorId = 1, Nombre = "Andres", Apellido = "Monsalve" }
        });

        var handler = new ListRepartidoresQueryHandler(unitOfWorkMock.Object);

        var result = await handler.Handle(new ListRepartidoresQuery(), CancellationToken.None);

        Assert.Single(result);
        Assert.Equal("Andres Monsalve", result[0].NombreCompleto);
    }
}