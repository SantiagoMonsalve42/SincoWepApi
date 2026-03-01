using Microsoft.EntityFrameworkCore;
using SincoWebApi.Domain.Entities;

namespace SincoWebApi.Infrastructure.Persistence;

public sealed class SincoDbContext : DbContext
{
    public SincoDbContext(DbContextOptions<SincoDbContext> options) : base(options)
    {
    }

    public DbSet<Prioridad> Prioridades => Set<Prioridad>();
    public DbSet<EstadoPaquete> EstadosPaquete => Set<EstadoPaquete>();
    public DbSet<Paquete> Paquetes => Set<Paquete>();
    public DbSet<Repartidor> Repartidores => Set<Repartidor>();
    public DbSet<PaqueteRepartidor> PaqueteRepartidores => Set<PaqueteRepartidor>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(SincoDbContext).Assembly);
    }
}