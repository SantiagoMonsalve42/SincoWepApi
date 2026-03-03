using SincoWebApi.Domain.Entities;
using SincoWebApi.Infrastructure.Repositories;
using SincoWebApi.UnitTest.Infrastructure.Persistence;

namespace SincoWebApi.UnitTest.Infrastructure.Repositories;

public sealed class EstadoPaqueteRepositoryTests
{
    [Fact]
    public async Task ListAsync_ReturnsData()
    {
        using var context = TestDbContextFactory.Create();
        var repository = new EstadoPaqueteRepository(context);

        await repository.AddAsync(new EstadoPaquete { EstadoId = 100, Descripcion = "Test" }, CancellationToken.None);
        await context.SaveChangesAsync(CancellationToken.None);

        var list = await repository.ListAsync(CancellationToken.None);

        Assert.Contains(list, x => x.EstadoId == 100);
    }
}