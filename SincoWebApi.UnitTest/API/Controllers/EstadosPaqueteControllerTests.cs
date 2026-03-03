using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SincoWebApi.Application.Dtos;
using SincoWebApi.Application.EstadosPaquete.Queries;
using SincoWepApi.Controllers;

namespace SincoWebApi.UnitTest.API.Controllers;

public sealed class EstadosPaqueteControllerTests
{
    [Fact]
    public async Task Get_ReturnsOk_WithEstadosPaquete()
    {
        var mediatorMock = new Mock<IMediator>();
        var controller = new EstadosPaqueteController(mediatorMock.Object);

        var expected = new List<EstadoPaqueteResponse>
        {
            new() { EstadoId = 1, Descripcion = "En bodega" },
            new() { EstadoId = 2, Descripcion = "Asignado" }
        };

        mediatorMock
            .Setup(m => m.Send(It.IsAny<ListEstadosPaqueteQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(expected);

        var result = await controller.Get(CancellationToken.None);

        var ok = Assert.IsType<OkObjectResult>(result.Result);
        var value = Assert.IsAssignableFrom<IReadOnlyList<EstadoPaqueteResponse>>(ok.Value);

        Assert.Equal(2, value.Count);

        mediatorMock.Verify(
            m => m.Send(It.IsAny<ListEstadosPaqueteQuery>(), It.IsAny<CancellationToken>()),
            Times.Once);
    }
}