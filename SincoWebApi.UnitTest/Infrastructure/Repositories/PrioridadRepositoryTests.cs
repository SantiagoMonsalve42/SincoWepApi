using SincoWebApi.Domain.Entities;
using SincoWebApi.Infrastructure.Repositories;
using SincoWebApi.UnitTest.Infrastructure.Persistence;

namespace SincoWebApi.UnitTest.Infrastructure.Repositories;

public sealed class PrioridadRepositoryTests
{
    [Fact]
    public async Task GetByIdAsync_ReturnsEntity()
    {
        using var context = TestDbContextFactory.Create();
        var repository = new PrioridadRepository(context);

        await repository.AddAsync(new Prioridad { PrioridadId = 200, Descripcion = "Test" }, CancellationToken.None);
        await context.SaveChangesAsync(CancellationToken.None);

        var entity = await repository.GetByIdAsync(200, CancellationToken.None);

        Assert.NotNull(entity);
        Assert.Equal("Test", entity.Descripcion);
    }
}