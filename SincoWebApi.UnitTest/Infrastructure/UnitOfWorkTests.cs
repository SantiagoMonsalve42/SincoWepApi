using SincoWebApi.Domain.Entities;
using SincoWebApi.Infrastructure;
using SincoWebApi.UnitTest.Infrastructure.Persistence;

namespace SincoWebApi.UnitTest.Infrastructure;

public sealed class UnitOfWorkTests
{
    [Fact]
    public void Constructor_InitializesRepositories()
    {
        using var context = TestDbContextFactory.Create();
        var uow = new UnitOfWork(context);

        Assert.NotNull(uow.Paquetes);
        Assert.NotNull(uow.Repartidores);
        Assert.NotNull(uow.PaqueteRepartidores);
        Assert.NotNull(uow.EstadosPaquete);
        Assert.NotNull(uow.Prioridades);
    }

    [Fact]
    public async Task SaveChangesAsync_PersistsChanges()
    {
        using var context = TestDbContextFactory.Create();
        var uow = new UnitOfWork(context);

        await uow.Prioridades.AddAsync(new Prioridad { PrioridadId = 99, Descripcion = "Test" }, CancellationToken.None);
        var affected = await uow.SaveChangesAsync(CancellationToken.None);

        Assert.Equal(1, affected);
    }
}