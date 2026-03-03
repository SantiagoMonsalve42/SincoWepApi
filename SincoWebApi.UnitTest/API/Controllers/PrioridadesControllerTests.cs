using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SincoWebApi.Application.Dtos;
using SincoWebApi.Application.Prioridades.Queries;
using SincoWepApi.Controllers;

namespace SincoWebApi.UnitTest.API.Controllers;

public sealed class PrioridadesControllerTests
{
    [Fact]
    public async Task Get_ReturnsOk_WithPrioridades()
    {
        var mediatorMock = new Mock<IMediator>();
        var controller = new PrioridadesController(mediatorMock.Object);

        var expected = new List<PrioridadResponse>
        {
            new() { PrioridadId = 1, Descripcion = "Alta" },
            new() { PrioridadId = 2, Descripcion = "Media" }
        };

        mediatorMock
            .Setup(m => m.Send(It.IsAny<ListPrioridadesQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(expected);

        var result = await controller.Get(CancellationToken.None);

        var ok = Assert.IsType<OkObjectResult>(result.Result);
        var value = Assert.IsAssignableFrom<IReadOnlyList<PrioridadResponse>>(ok.Value);

        Assert.Equal(2, value.Count);

        mediatorMock.Verify(
            m => m.Send(It.IsAny<ListPrioridadesQuery>(), It.IsAny<CancellationToken>()),
            Times.Once);
    }
}