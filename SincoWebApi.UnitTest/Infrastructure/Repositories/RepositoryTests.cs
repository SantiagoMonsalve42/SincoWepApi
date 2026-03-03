using SincoWebApi.Domain.Entities;
using SincoWebApi.Infrastructure.Repositories;
using SincoWebApi.UnitTest.Infrastructure.Persistence;

namespace SincoWebApi.UnitTest.Infrastructure.Repositories;

public sealed class RepositoryTests
{
    [Fact]
    public async Task AddAsync_AndGetByIdAsync_WorkCorrectly()
    {
        using var context = TestDbContextFactory.Create();
        var repository = new PrioridadRepository(context);

        await repository.AddAsync(new Prioridad { PrioridadId = 50, Descripcion = "Urgente" }, CancellationToken.None);
        await context.SaveChangesAsync(CancellationToken.None);

        var entity = await repository.GetByIdAsync(50, CancellationToken.None);

        Assert.NotNull(entity);
        Assert.Equal("Urgente", entity.Descripcion);
    }

    [Fact]
    public async Task ListAsync_ReturnsEntities()
    {
        using var context = TestDbContextFactory.Create();
        var repository = new PrioridadRepository(context);

        await repository.AddAsync(new Prioridad { PrioridadId = 60, Descripcion = "Alta" }, CancellationToken.None);
        await repository.AddAsync(new Prioridad { PrioridadId = 61, Descripcion = "Media" }, CancellationToken.None);
        await context.SaveChangesAsync(CancellationToken.None);

        var list = await repository.ListAsync(CancellationToken.None);

        Assert.True(list.Count >= 2);
    }

    [Fact]
    public async Task Update_MarksEntityAsModified()
    {
        using var context = TestDbContextFactory.Create();
        var repository = new PrioridadRepository(context);

        await repository.AddAsync(new Prioridad { PrioridadId = 70, Descripcion = "Inicial" }, CancellationToken.None);
        await context.SaveChangesAsync(CancellationToken.None);

        var entity = await repository.GetByIdAsync(70, CancellationToken.None);
        Assert.NotNull(entity);

        entity.Descripcion = "Actualizada";
        repository.Update(entity);
        await context.SaveChangesAsync(CancellationToken.None);

        var updated = await repository.GetByIdAsync(70, CancellationToken.None);
        Assert.Equal("Actualizada", updated?.Descripcion);
    }
}