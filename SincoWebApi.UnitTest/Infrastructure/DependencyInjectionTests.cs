using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SincoWebApi.Domain.Interfaces;
using SincoWebApi.Domain.Interfaces.Repositories;
using SincoWebApi.Infrastructure;
using SincoWebApi.Infrastructure.Repositories;
using SincoWebApi.Infrastructure.Services;

namespace SincoWebApi.UnitTest.Infrastructure;

public sealed class DependencyInjectionTests
{
    [Fact]
    public void AddInfrastructure_RegistersExpectedServices()
    {
        var services = new ServiceCollection();
        var configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string?>
            {
                ["ConnectionStrings:DefaultConnection"] = "Server=(localdb)\\MSSQLLocalDB;Database=SincoWebApiTest;Trusted_Connection=True;"
            })
            .Build();

        services.AddInfrastructure(configuration);

        Assert.Contains(services, sd => sd.ServiceType == typeof(IPaqueteRepository) && sd.ImplementationType == typeof(PaqueteRepository));
        Assert.Contains(services, sd => sd.ServiceType == typeof(IRepartidorRepository) && sd.ImplementationType == typeof(RepartidorRepository));
        Assert.Contains(services, sd => sd.ServiceType == typeof(IPaqueteRepartidorRepository) && sd.ImplementationType == typeof(PaqueteRepartidorRepository));
        Assert.Contains(services, sd => sd.ServiceType == typeof(IEstadoPaqueteRepository) && sd.ImplementationType == typeof(EstadoPaqueteRepository));
        Assert.Contains(services, sd => sd.ServiceType == typeof(IPrioridadRepository) && sd.ImplementationType == typeof(PrioridadRepository));
        Assert.Contains(services, sd => sd.ServiceType == typeof(IUnitOfWork) && sd.ImplementationType == typeof(UnitOfWork));
        Assert.Contains(services, sd => sd.ServiceType == typeof(ICodigoSeguimientoGenerator) && sd.ImplementationType == typeof(CodigoSeguimientoGenerator));
    }
}