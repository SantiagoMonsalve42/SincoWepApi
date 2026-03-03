using SincoWebApi.Domain.Entities;

namespace SincoWebApi.UnitTest.Domain.Entities;

public sealed class EstadoPaqueteTests
{
    [Fact]
    public void Constructor_InitializesDefaultValues()
    {
        var entity = new EstadoPaquete();

        Assert.Equal(0, entity.EstadoId);
        Assert.Equal(string.Empty, entity.Descripcion);
        Assert.NotNull(entity.Paquetes);
        Assert.Empty(entity.Paquetes);
    }
}