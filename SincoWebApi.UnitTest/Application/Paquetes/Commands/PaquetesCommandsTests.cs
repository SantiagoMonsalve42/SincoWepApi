using MediatR;
using SincoWebApi.Application.Dtos;
using SincoWebApi.Application.Paquetes.Commands;

namespace SincoWebApi.UnitTest.Application.Paquetes.Commands;

public sealed class PaquetesCommandsTests
{
    [Fact]
    public void CreatePaqueteCommand_ShouldStoreRequest()
    {
        var request = new PaqueteCreateRequest { Descripcion = "Caja", Peso = 1m, PrioridadId = 1 };
        var command = new CreatePaqueteCommand(request);

        Assert.Same(request, command.Request);
        Assert.IsAssignableFrom<IRequest<PaqueteResponse>>(command);
    }

    [Fact]
    public void AssignRepartidorCommand_ShouldStoreData()
    {
        var request = new PaqueteAssignRequest { RepartidorId = 5 };
        var command = new AssignRepartidorCommand(10, request);

        Assert.Equal(10, command.PaqueteId);
        Assert.Same(request, command.Request);
        Assert.IsAssignableFrom<IRequest<bool>>(command);
    }

    [Fact]
    public void DeliverPaqueteCommand_ShouldStorePaqueteId()
    {
        var command = new DeliverPaqueteCommand(99);

        Assert.Equal(99, command.PaqueteId);
        Assert.IsAssignableFrom<IRequest<bool>>(command);
    }
}