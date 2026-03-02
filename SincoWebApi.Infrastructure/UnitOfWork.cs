using SincoWebApi.Domain.Interfaces;
using SincoWebApi.Domain.Interfaces.Repositories;
using SincoWebApi.Infrastructure.Persistence;
using SincoWebApi.Infrastructure.Repositories;

namespace SincoWebApi.Infrastructure;

public sealed class UnitOfWork : IUnitOfWork
{
    private readonly SincoDbContext _context;

    public UnitOfWork(SincoDbContext context)
    {
        _context = context;
        Paquetes = new PaqueteRepository(context);
        Repartidores = new RepartidorRepository(context);
        PaqueteRepartidores = new PaqueteRepartidorRepository(context);
        EstadosPaquete = new EstadoPaqueteRepository(context);
        Prioridades = new PrioridadRepository(context);
    }

    public IPaqueteRepository Paquetes { get; }
    public IRepartidorRepository Repartidores { get; }
    public IPaqueteRepartidorRepository PaqueteRepartidores { get; }
    public IEstadoPaqueteRepository EstadosPaquete { get; }
    public IPrioridadRepository Prioridades { get; }

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken)
    {
        return _context.SaveChangesAsync(cancellationToken);
    }
}