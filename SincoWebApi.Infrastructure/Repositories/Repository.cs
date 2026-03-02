using Microsoft.EntityFrameworkCore;
using SincoWebApi.Domain.Interfaces.Repositories;
using SincoWebApi.Infrastructure.Persistence;

namespace SincoWebApi.Infrastructure.Repositories;

public class Repository<T> : IRepository<T> where T : class
{
    protected readonly SincoDbContext Context;

    protected Repository(SincoDbContext context)
    {
        Context = context;
    }

    public async Task<T?> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        return await Context.Set<T>().FindAsync([id], cancellationToken);
    }

    public async Task<IReadOnlyList<T>> ListAsync(CancellationToken cancellationToken)
    {
        return await Context.Set<T>().AsNoTracking().ToListAsync(cancellationToken);
    }

    public async Task AddAsync(T entity, CancellationToken cancellationToken)
    {
        await Context.Set<T>().AddAsync(entity, cancellationToken);
    }

    public void Update(T entity)
    {
        Context.Set<T>().Update(entity);
    }
}