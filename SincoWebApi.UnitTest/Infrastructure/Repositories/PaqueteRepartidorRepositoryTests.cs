using SincoWebApi.Domain.Constants;
using SincoWebApi.Domain.Entities;
using SincoWebApi.Infrastructure.Repositories;
using SincoWebApi.UnitTest.Infrastructure.Persistence;

namespace SincoWebApi.UnitTest.Infrastructure.Repositories;

public sealed class PaqueteRepartidorRepositoryTests
{
    [Fact]
    public async Task CountAsignadosPorRepartidorAsync_CountsOnlyAssignedPaquetes()
    {
        using var context = TestDbContextFactory.Create();
        var repository = new PaqueteRepartidorRepository(context);

        var assigned = new Paquete { EstadoId = EstadoPaqueteIds.Asignado, Descripcion = "A", Peso = 1, CodigoSeguimiento = "A1", PrioridadId = 1 };
        var delivered = new Paquete { EstadoId = EstadoPaqueteIds.Entregado, Descripcion = "B", Peso = 1, CodigoSeguimiento = "B1", PrioridadId = 1 };

        context.Prioridades.Add(new Prioridad { PrioridadId = 1, Descripcion = "Alta" });
        context.EstadosPaquete.Add(new EstadoPaquete { EstadoId = EstadoPaqueteIds.Asignado, Descripcion = "Asignado" });
        context.EstadosPaquete.Add(new EstadoPaquete { EstadoId = EstadoPaqueteIds.Entregado, Descripcion = "Entregado" });
        context.Paquetes.AddRange(assigned, delivered);

        context.PaqueteRepartidores.Add(new PaqueteRepartidor { RepartidorId = 10, Paquete = assigned });
        context.PaqueteRepartidores.Add(new PaqueteRepartidor { RepartidorId = 10, Paquete = delivered });
        context.PaqueteRepartidores.Add(new PaqueteRepartidor { RepartidorId = 20, Paquete = assigned });

        await context.SaveChangesAsync(CancellationToken.None);

        var count = await repository.CountAsignadosPorRepartidorAsync(10, CancellationToken.None);

        Assert.Equal(1, count);
    }

    [Fact]
    public async Task AddAsync_AddsEntity()
    {
        using var context = TestDbContextFactory.Create();
        var repository = new PaqueteRepartidorRepository(context);

        var entity = new PaqueteRepartidor { RepartidorId = 1, PaqueteId = 1 };
        await repository.AddAsync(entity, CancellationToken.None);
        await context.SaveChangesAsync(CancellationToken.None);

        Assert.Single(context.PaqueteRepartidores);
    }
}