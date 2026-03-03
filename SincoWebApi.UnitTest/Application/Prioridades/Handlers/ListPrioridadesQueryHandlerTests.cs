using Moq;
using SincoWebApi.Application.Prioridades.Handlers;
using SincoWebApi.Application.Prioridades.Queries;
using SincoWebApi.Domain.Entities;
using SincoWebApi.Domain.Interfaces;
using SincoWebApi.Domain.Interfaces.Repositories;

namespace SincoWebApi.UnitTest.Application.Prioridades.Handlers;

public sealed class ListPrioridadesQueryHandlerTests
{
    [Fact]
    public async Task Handle_ShouldReturnMappedPrioridades()
    {
        var repoMock = new Mock<IPrioridadRepository>();
        var unitOfWorkMock = new Mock<IUnitOfWork>();

        unitOfWorkMock.SetupGet(x => x.Prioridades).Returns(repoMock.Object);
        repoMock.Setup(x => x.ListAsync(It.IsAny<CancellationToken>())).ReturnsAsync(new List<Prioridad>
        {
            new() { PrioridadId = 1, Descripcion = "Alta" },
            new() { PrioridadId = 2, Descripcion = "Media" }
        });

        var handler = new ListPrioridadesQueryHandler(unitOfWorkMock.Object);

        var result = await handler.Handle(new ListPrioridadesQuery(), CancellationToken.None);

        Assert.Equal(2, result.Count);
        Assert.Equal("Alta", result[0].Descripcion);
    }
}