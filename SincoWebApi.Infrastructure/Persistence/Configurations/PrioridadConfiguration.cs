using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SincoWebApi.Domain.Entities;

namespace SincoWebApi.Infrastructure.Persistence.Configurations;

public sealed class PrioridadConfiguration : IEntityTypeConfiguration<Prioridad>
{
    public void Configure(EntityTypeBuilder<Prioridad> builder)
    {
        builder.ToTable("tblPrioridad");
        builder.HasKey(x => x.PrioridadId);

        builder.Property(x => x.PrioridadId)
            .ValueGeneratedNever();

        builder.Property(x => x.Descripcion)
            .HasMaxLength(250)
            .IsRequired();

        builder.HasData(
            new Prioridad { PrioridadId = 1, Descripcion = "Alta" },
            new Prioridad { PrioridadId = 2, Descripcion = "Media" },
            new Prioridad { PrioridadId = 3, Descripcion = "Baja" }
        );
    }
}