using SincoWebApi.Domain.Entities;
using SincoWebApi.Domain.Interfaces.Repositories;
using SincoWebApi.Infrastructure.Persistence;

namespace SincoWebApi.Infrastructure.Repositories;

public sealed class PrioridadRepository : Repository<Prioridad>, IPrioridadRepository
{
    public PrioridadRepository(SincoDbContext context) : base(context)
    {
    }
}