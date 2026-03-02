using Microsoft.Extensions.DependencyInjection;
using SincoWebApi.Application.Services;

namespace SincoWebApi.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IPaqueteService, PaqueteService>();
        services.AddScoped<IRepartidorService, RepartidorService>();
        services.AddScoped<IEstadoPaqueteService, EstadoPaqueteService>();
        services.AddScoped<IPrioridadService, PrioridadService>();

        return services;
    }
}