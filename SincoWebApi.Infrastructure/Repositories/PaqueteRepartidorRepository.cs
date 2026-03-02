using Microsoft.EntityFrameworkCore;
using SincoWebApi.Domain.Constants;
using SincoWebApi.Domain.Entities;
using SincoWebApi.Domain.Interfaces.Repositories;
using SincoWebApi.Infrastructure.Persistence;

namespace SincoWebApi.Infrastructure.Repositories;

public sealed class PaqueteRepartidorRepository : IPaqueteRepartidorRepository
{
    private readonly SincoDbContext _context;

    public PaqueteRepartidorRepository(SincoDbContext context)
    {
        _context = context;
    }

    public async Task<int> CountAsignadosPorRepartidorAsync(int repartidorId, CancellationToken cancellationToken)
    {
        return await _context.PaqueteRepartidores
            .CountAsync(
                pr => pr.RepartidorId == repartidorId && pr.Paquete.EstadoId == EstadoPaqueteIds.Asignado,
                cancellationToken);
    }

    public async Task AddAsync(PaqueteRepartidor entity, CancellationToken cancellationToken)
    {
        await _context.PaqueteRepartidores.AddAsync(entity, cancellationToken);
    }
}