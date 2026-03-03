using SincoWebApi.Domain.Entities;
using SincoWebApi.Infrastructure.Repositories;
using SincoWebApi.UnitTest.Infrastructure.Persistence;

namespace SincoWebApi.UnitTest.Infrastructure.Repositories;

public sealed class RepartidorRepositoryTests
{
    [Fact]
    public async Task AddAsync_AndListAsync_WorkCorrectly()
    {
        using var context = TestDbContextFactory.Create();
        var repository = new RepartidorRepository(context);

        await repository.AddAsync(new Repartidor { RepartidorId = 300, Nombre = "Nombre", Apellido = "Apellido" }, CancellationToken.None);
        await context.SaveChangesAsync(CancellationToken.None);

        var list = await repository.ListAsync(CancellationToken.None);

        Assert.Contains(list, x => x.RepartidorId == 300);
    }
}