using SincoWebApi.Domain.Entities;

namespace SincoWebApi.UnitTest.Domain.Entities;

public sealed class PaqueteRepartidorTests
{
    [Fact]
    public void Properties_CanBeAssigned()
    {
        var paquete = new Paquete { PaqueteId = 10 };
        var repartidor = new Repartidor { RepartidorId = 20 };

        var entity = new PaqueteRepartidor
        {
            PaqueteRepartidorId = 1,
            PaqueteId = paquete.PaqueteId,
            Paquete = paquete,
            RepartidorId = repartidor.RepartidorId,
            Repartidor = repartidor
        };

        Assert.Equal(1, entity.PaqueteRepartidorId);
        Assert.Equal(10, entity.PaqueteId);
        Assert.Same(paquete, entity.Paquete);
        Assert.Equal(20, entity.RepartidorId);
        Assert.Same(repartidor, entity.Repartidor);
    }
}