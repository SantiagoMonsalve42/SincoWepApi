using Microsoft.EntityFrameworkCore;
using SincoWebApi.Domain.Entities;
using SincoWebApi.Domain.Interfaces.Repositories;
using SincoWebApi.Infrastructure.Persistence;

namespace SincoWebApi.Infrastructure.Repositories;

public sealed class PaqueteRepository : Repository<Paquete>, IPaqueteRepository
{
    public PaqueteRepository(SincoDbContext context) : base(context)
    {
    }

    public async Task<bool> ExistsByCodigoSeguimientoAsync(string codigoSeguimiento, CancellationToken cancellationToken)
    {
        return await Context.Paquetes.AnyAsync(
            paquete => paquete.CodigoSeguimiento == codigoSeguimiento,
            cancellationToken);
    }

    public async Task<IReadOnlyList<Paquete>> ListByEstadoAsync(int? estadoId, CancellationToken cancellationToken)
    {
        var query = Context.Paquetes
            .Include(paquete => paquete.EstadoPaquete)
            .Include(paquete => paquete.Prioridad)
            .AsNoTracking();

        if (estadoId.HasValue)
        {
            query = query.Where(paquete => paquete.EstadoId == estadoId.Value);
        }

        return await query.ToListAsync(cancellationToken);
    }

    public async Task<Paquete?> GetByIdWithDetailsAsync(int paqueteId, CancellationToken cancellationToken)
    {
        return await Context.Paquetes
            .Include(paquete => paquete.EstadoPaquete)
            .Include(paquete => paquete.Prioridad)
            .FirstOrDefaultAsync(paquete => paquete.PaqueteId == paqueteId, cancellationToken);
    }
}