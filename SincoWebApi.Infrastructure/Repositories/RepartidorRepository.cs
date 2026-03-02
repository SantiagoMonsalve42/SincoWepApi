using SincoWebApi.Domain.Entities;
using SincoWebApi.Domain.Interfaces.Repositories;
using SincoWebApi.Infrastructure.Persistence;

namespace SincoWebApi.Infrastructure.Repositories;

public sealed class RepartidorRepository : Repository<Repartidor>, IRepartidorRepository
{
    public RepartidorRepository(SincoDbContext context) : base(context)
    {
    }
}