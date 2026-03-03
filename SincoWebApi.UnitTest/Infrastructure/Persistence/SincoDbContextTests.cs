using SincoWebApi.Domain.Entities;
using SincoWebApi.Infrastructure.Persistence;

namespace SincoWebApi.UnitTest.Infrastructure.Persistence;

public sealed class SincoDbContextTests
{
    [Fact]
    public void DbSets_AreAvailable()
    {
        using var context = TestDbContextFactory.Create();

        Assert.NotNull(context.Prioridades);
        Assert.NotNull(context.EstadosPaquete);
        Assert.NotNull(context.Paquetes);
        Assert.NotNull(context.Repartidores);
        Assert.NotNull(context.PaqueteRepartidores);
    }

    [Fact]
    public void Model_AppliesEntityConfigurations()
    {
        using var context = TestDbContextFactory.Create();

        var prioridad = context.Model.FindEntityType(typeof(Prioridad));
        var estado = context.Model.FindEntityType(typeof(EstadoPaquete));
        var paquete = context.Model.FindEntityType(typeof(Paquete));
        var repartidor = context.Model.FindEntityType(typeof(Repartidor));
        var paqueteRepartidor = context.Model.FindEntityType(typeof(PaqueteRepartidor));

        Assert.NotNull(prioridad);
        Assert.NotNull(estado);
        Assert.NotNull(paquete);
        Assert.NotNull(repartidor);
        Assert.NotNull(paqueteRepartidor);
    }
}