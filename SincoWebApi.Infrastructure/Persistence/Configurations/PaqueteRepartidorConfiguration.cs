using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SincoWebApi.Domain.Entities;

namespace SincoWebApi.Infrastructure.Persistence.Configurations;

public sealed class PaqueteRepartidorConfiguration : IEntityTypeConfiguration<PaqueteRepartidor>
{
    public void Configure(EntityTypeBuilder<PaqueteRepartidor> builder)
    {
        builder.ToTable("tblPaqueteRepartidor");
        builder.HasKey(x => x.PaqueteRepartidorId);

        builder.Property(x => x.PaqueteRepartidorId)
            .ValueGeneratedOnAdd();

        builder.HasOne(x => x.Paquete)
            .WithMany(x => x.PaqueteRepartidores)
            .HasForeignKey(x => x.PaqueteId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Repartidor)
            .WithMany(x => x.PaqueteRepartidores)
            .HasForeignKey(x => x.RepartidorId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}