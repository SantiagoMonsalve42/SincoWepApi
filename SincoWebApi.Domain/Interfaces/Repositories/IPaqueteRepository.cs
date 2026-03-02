using SincoWebApi.Domain.Entities;

namespace SincoWebApi.Domain.Interfaces.Repositories;

public interface IPaqueteRepository : IRepository<Paquete>
{
    Task<IReadOnlyList<Paquete>> ListByEstadoAsync(int? estadoId, CancellationToken cancellationToken);
    Task<Paquete?> GetByIdWithDetailsAsync(int paqueteId, CancellationToken cancellationToken);
}