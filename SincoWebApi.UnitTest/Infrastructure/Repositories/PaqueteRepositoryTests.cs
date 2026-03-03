using SincoWebApi.Domain.Constants;
using SincoWebApi.Domain.Entities;
using SincoWebApi.Infrastructure.Repositories;
using SincoWebApi.UnitTest.Infrastructure.Persistence;

namespace SincoWebApi.UnitTest.Infrastructure.Repositories;

public sealed class PaqueteRepositoryTests
{
    [Fact]
    public async Task ExistsByCodigoSeguimientoAsync_ReturnsTrue_WhenCodeExists()
    {
        using var context = TestDbContextFactory.Create();
        await SeedCatalogsAsync(context);

        var repository = new PaqueteRepository(context);
        await repository.AddAsync(new Paquete
        {
            Descripcion = "Caja",
            Peso = 1.5m,
            CodigoSeguimiento = "ABC123",
            PrioridadId = 1,
            EstadoId = EstadoPaqueteIds.EnBodega
        }, CancellationToken.None);
        await context.SaveChangesAsync(CancellationToken.None);

        var exists = await repository.ExistsByCodigoSeguimientoAsync("ABC123", CancellationToken.None);

        Assert.True(exists);
    }

    [Fact]
    public async Task ListByEstadoAsync_FiltersAndOrdersDescending()
    {
        using var context = TestDbContextFactory.Create();
        await SeedCatalogsAsync(context);

        var repository = new PaqueteRepository(context);

        await repository.AddAsync(new Paquete
        {
            Descripcion = "P1",
            Peso = 1m,
            CodigoSeguimiento = "COD1",
            PrioridadId = 1,
            EstadoId = EstadoPaqueteIds.EnBodega
        }, CancellationToken.None);

        await repository.AddAsync(new Paquete
        {
            Descripcion = "P2",
            Peso = 2m,
            CodigoSeguimiento = "COD2",
            PrioridadId = 1,
            EstadoId = EstadoPaqueteIds.Asignado
        }, CancellationToken.None);

        await repository.AddAsync(new Paquete
        {
            Descripcion = "P3",
            Peso = 3m,
            CodigoSeguimiento = "COD3",
            PrioridadId = 1,
            EstadoId = EstadoPaqueteIds.Asignado
        }, CancellationToken.None);

        await context.SaveChangesAsync(CancellationToken.None);

        var assigned = await repository.ListByEstadoAsync(EstadoPaqueteIds.Asignado, CancellationToken.None);

        Assert.Equal(2, assigned.Count);
        Assert.True(assigned[0].PaqueteId > assigned[1].PaqueteId);
    }

    [Fact]
    public async Task GetByIdWithDetailsAsync_ReturnsEntity_WhenExists()
    {
        using var context = TestDbContextFactory.Create();
        await SeedCatalogsAsync(context);

        var repository = new PaqueteRepository(context);

        var paquete = new Paquete
        {
            Descripcion = "Detalle",
            Peso = 1m,
            CodigoSeguimiento = "DET001",
            PrioridadId = 1,
            EstadoId = EstadoPaqueteIds.EnBodega
        };

        await repository.AddAsync(paquete, CancellationToken.None);
        await context.SaveChangesAsync(CancellationToken.None);

        var result = await repository.GetByIdWithDetailsAsync(paquete.PaqueteId, CancellationToken.None);

        Assert.NotNull(result);
        Assert.NotNull(result.Prioridad);
        Assert.NotNull(result.EstadoPaquete);
    }

    private static async Task SeedCatalogsAsync(SincoWebApi.Infrastructure.Persistence.SincoDbContext context)
    {
        context.Prioridades.Add(new Prioridad { PrioridadId = 1, Descripcion = "Alta" });
        context.EstadosPaquete.Add(new EstadoPaquete { EstadoId = EstadoPaqueteIds.EnBodega, Descripcion = "En bodega" });
        context.EstadosPaquete.Add(new EstadoPaquete { EstadoId = EstadoPaqueteIds.Asignado, Descripcion = "Asignado" });
        await context.SaveChangesAsync(CancellationToken.None);
    }
}