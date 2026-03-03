using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SincoWebApi.Application.Dtos;
using SincoWebApi.Application.Repartidores.Queries;
using SincoWepApi.Controllers;

namespace SincoWebApi.UnitTest.API.Controllers;

public sealed class RepartidoresControllerTests
{
    [Fact]
    public async Task Get_ReturnsOk_WithRepartidores()
    {
        var mediatorMock = new Mock<IMediator>();
        var controller = new RepartidoresController(mediatorMock.Object);

        var expected = new List<RepartidorResponse>
        {
            new() { RepartidorId = 1, NombreCompleto = "Andres Monsalve" },
            new() { RepartidorId = 2, NombreCompleto = "Juan Chavez" }
        };

        mediatorMock
            .Setup(m => m.Send(It.IsAny<ListRepartidoresQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(expected);

        var result = await controller.Get(CancellationToken.None);

        var ok = Assert.IsType<OkObjectResult>(result.Result);
        var value = Assert.IsAssignableFrom<IReadOnlyList<RepartidorResponse>>(ok.Value);
        Assert.Equal(2, value.Count);

        mediatorMock.Verify(
            m => m.Send(It.IsAny<ListRepartidoresQuery>(), It.IsAny<CancellationToken>()),
            Times.Once);
    }
}