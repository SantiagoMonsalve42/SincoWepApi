using Microsoft.EntityFrameworkCore;
using SincoWebApi.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore.InMemory;

namespace SincoWebApi.UnitTest.Infrastructure.Persistence;

internal static class TestDbContextFactory
{
    public static SincoDbContext Create(string? databaseName = null)
    {
        var options = new DbContextOptionsBuilder<SincoDbContext>()
            .UseInMemoryDatabase(databaseName ?? Guid.NewGuid().ToString())
            .Options;

        return new SincoDbContext(options);
    }
}