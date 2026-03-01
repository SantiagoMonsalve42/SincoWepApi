using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SincoWebApi.Domain.Entities;

namespace SincoWebApi.Infrastructure.Persistence.Configurations;

public sealed class RepartidorConfiguration : IEntityTypeConfiguration<Repartidor>
{
    public void Configure(EntityTypeBuilder<Repartidor> builder)
    {
        builder.ToTable("tblRepartidor");
        builder.HasKey(x => x.RepartidorId);

        builder.Property(x => x.RepartidorId)
            .ValueGeneratedNever();

        builder.Property(x => x.Nombre)
            .HasMaxLength(500)
            .IsRequired();

        builder.Property(x => x.Apellido)
            .HasMaxLength(500)
            .IsRequired();

        builder.HasData(
            new Repartidor { RepartidorId = 1, Nombre = "Andres", Apellido = "Monsalve" },
            new Repartidor { RepartidorId = 2, Nombre = "Juan", Apellido = "Chavez" },
            new Repartidor { RepartidorId = 3, Nombre = "Esteban", Apellido = "Ortiz" },
            new Repartidor { RepartidorId = 4, Nombre = "Andres", Apellido = "Quiñones" },
            new Repartidor { RepartidorId = 5, Nombre = "Laura", Apellido = "Quiroga" }
        );
    }
}