using SincoWebApi.Domain.Entities;

namespace SincoWebApi.UnitTest.Domain.Entities;

public sealed class PrioridadTests
{
    [Fact]
    public void Constructor_InitializesDefaultValues()
    {
        var entity = new Prioridad();

        Assert.Equal(0, entity.PrioridadId);
        Assert.Equal(string.Empty, entity.Descripcion);
        Assert.NotNull(entity.Paquetes);
        Assert.Empty(entity.Paquetes);
    }
}