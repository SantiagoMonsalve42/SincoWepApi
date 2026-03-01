using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SincoWebApi.Domain.Entities;

namespace SincoWebApi.Infrastructure.Persistence.Configurations;

public sealed class EstadoPaqueteConfiguration : IEntityTypeConfiguration<EstadoPaquete>
{
    public void Configure(EntityTypeBuilder<EstadoPaquete> builder)
    {
        builder.ToTable("tblEstadoPaquete");
        builder.HasKey(x => x.EstadoId);

        builder.Property(x => x.EstadoId)
            .ValueGeneratedNever();

        builder.Property(x => x.Descripcion)
            .HasMaxLength(250)
            .IsRequired();

        builder.HasData(
            new EstadoPaquete { EstadoId = 1, Descripcion = "En bodega" },
            new EstadoPaquete { EstadoId = 2, Descripcion = "Asignado" },
            new EstadoPaquete { EstadoId = 3, Descripcion = "Entregado" }
        );
    }
}