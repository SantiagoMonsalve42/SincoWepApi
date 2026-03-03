using SincoWebApi.Domain.Entities;

namespace SincoWebApi.UnitTest.Domain.Entities;

public sealed class RepartidorTests
{
    [Fact]
    public void Constructor_InitializesDefaultValues()
    {
        var entity = new Repartidor();

        Assert.Equal(0, entity.RepartidorId);
        Assert.Equal(string.Empty, entity.Nombre);
        Assert.Equal(string.Empty, entity.Apellido);
        Assert.NotNull(entity.PaqueteRepartidores);
        Assert.Empty(entity.PaqueteRepartidores);
    }
}