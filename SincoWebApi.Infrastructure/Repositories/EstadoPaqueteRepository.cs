using SincoWebApi.Domain.Entities;
using SincoWebApi.Domain.Interfaces.Repositories;
using SincoWebApi.Infrastructure.Persistence;

namespace SincoWebApi.Infrastructure.Repositories;

public sealed class EstadoPaqueteRepository : Repository<EstadoPaquete>, IEstadoPaqueteRepository
{
    public EstadoPaqueteRepository(SincoDbContext context) : base(context)
    {
    }
}