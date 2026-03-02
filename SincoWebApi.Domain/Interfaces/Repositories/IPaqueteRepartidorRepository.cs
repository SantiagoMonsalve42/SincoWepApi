using SincoWebApi.Domain.Entities;

namespace SincoWebApi.Domain.Interfaces.Repositories;

public interface IPaqueteRepartidorRepository
{
    Task<int> CountAsignadosPorRepartidorAsync(int repartidorId, CancellationToken cancellationToken);
    Task AddAsync(PaqueteRepartidor entity, CancellationToken cancellationToken);
}