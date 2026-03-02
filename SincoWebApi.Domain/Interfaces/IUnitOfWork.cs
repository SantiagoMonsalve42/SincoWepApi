using SincoWebApi.Domain.Interfaces.Repositories;

namespace SincoWebApi.Domain.Interfaces;

public interface IUnitOfWork
{
    IPaqueteRepository Paquetes { get; }
    IRepartidorRepository Repartidores { get; }
    IPaqueteRepartidorRepository PaqueteRepartidores { get; }
    IEstadoPaqueteRepository EstadosPaquete { get; }
    IPrioridadRepository Prioridades { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}