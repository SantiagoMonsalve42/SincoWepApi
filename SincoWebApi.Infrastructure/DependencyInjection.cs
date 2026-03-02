using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SincoWebApi.Domain.Interfaces;
using SincoWebApi.Domain.Interfaces.Repositories;
using SincoWebApi.Infrastructure.Persistence;
using SincoWebApi.Infrastructure.Repositories;
using SincoWebApi.Infrastructure.Services;

namespace SincoWebApi.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<SincoDbContext>(options =>
            options.UseSqlServer(
                configuration.GetConnectionString("DefaultConnection"),
                sql => sql.MigrationsAssembly(typeof(SincoDbContext).Assembly.FullName)));

        services.AddScoped<IPaqueteRepository, PaqueteRepository>();
        services.AddScoped<IRepartidorRepository, RepartidorRepository>();
        services.AddScoped<IPaqueteRepartidorRepository, PaqueteRepartidorRepository>();
        services.AddScoped<IEstadoPaqueteRepository, EstadoPaqueteRepository>();
        services.AddScoped<IPrioridadRepository, PrioridadRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<ICodigoSeguimientoGenerator, CodigoSeguimientoGenerator>();

        return services;
    }
}