using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SincoWebApi.Domain.Entities;

namespace SincoWebApi.Infrastructure.Persistence.Configurations;

public sealed class PaqueteConfiguration : IEntityTypeConfiguration<Paquete>
{
    public void Configure(EntityTypeBuilder<Paquete> builder)
    {
        builder.ToTable("tblPaquete");
        builder.HasKey(x => x.PaqueteId);

        builder.Property(x => x.PaqueteId)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.Descripcion)
            .HasColumnType("nvarchar(max)")
            .IsRequired();

        builder.Property(x => x.Peso)
            .HasColumnType("decimal(18,2)")
            .IsRequired();

        builder.Property(x => x.CodigoSeguimiento)
            .HasMaxLength(250)
            .IsUnicode(false)
            .IsRequired();

        builder.HasOne(x => x.Prioridad)
            .WithMany(x => x.Paquetes)
            .HasForeignKey(x => x.PrioridadId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.EstadoPaquete)
            .WithMany(x => x.Paquetes)
            .HasForeignKey(x => x.EstadoId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}