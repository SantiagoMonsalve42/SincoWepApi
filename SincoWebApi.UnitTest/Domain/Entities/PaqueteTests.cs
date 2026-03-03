using SincoWebApi.Domain.Constants;
using SincoWebApi.Domain.Entities;
using SincoWebApi.Domain.Exceptions;

namespace SincoWebApi.UnitTest.Domain.Entities;

public sealed class PaqueteTests
{
    [Fact]
    public void Constructor_InitializesDefaultValues()
    {
        var entity = new Paquete();

        Assert.Equal(0, entity.PaqueteId);
        Assert.Equal(string.Empty, entity.Descripcion);
        Assert.Equal(0m, entity.Peso);
        Assert.Equal(string.Empty, entity.CodigoSeguimiento);
        Assert.Equal(0, entity.PrioridadId);
        Assert.Equal(0, entity.EstadoId);
        Assert.NotNull(entity.PaqueteRepartidores);
        Assert.Empty(entity.PaqueteRepartidores);
    }

    [Fact]
    public void AssignRepartidor_Throws_WhenEstadoIsNotEnBodega()
    {
        var paquete = new Paquete
        {
            PaqueteId = 5,
            EstadoId = EstadoPaqueteIds.Asignado
        };

        var exception = Assert.Throws<BusinessRuleException>(() => paquete.AssignRepartidor(repartidorId: 1, asignados: 0));

        Assert.Contains("en bodega", exception.Message);
    }

    [Fact]
    public void AssignRepartidor_Throws_WhenRepartidorHasThreeOrMoreAssigned()
    {
        var paquete = new Paquete
        {
            PaqueteId = 5,
            EstadoId = EstadoPaqueteIds.EnBodega
        };

        var exception = Assert.Throws<BusinessRuleException>(() => paquete.AssignRepartidor(repartidorId: 1, asignados: 3));

        Assert.Contains("aceptar", exception.Message);
    }

    [Fact]
    public void AssignRepartidor_AddsAssignment_AndChangesEstado()
    {
        var paquete = new Paquete
        {
            PaqueteId = 7,
            EstadoId = EstadoPaqueteIds.EnBodega
        };

        var asignacion = paquete.AssignRepartidor(repartidorId: 11, asignados: 2);

        Assert.Equal(EstadoPaqueteIds.Asignado, paquete.EstadoId);
        Assert.Single(paquete.PaqueteRepartidores);

        Assert.Equal(7, asignacion.PaqueteId);
        Assert.Equal(11, asignacion.RepartidorId);
        Assert.Same(paquete, asignacion.Paquete);
        Assert.Same(asignacion, paquete.PaqueteRepartidores.Single());
    }

    [Fact]
    public void MoveToEntregado_Throws_WhenEstadoIsNotAsignado()
    {
        var paquete = new Paquete
        {
            EstadoId = EstadoPaqueteIds.EnBodega
        };

        var exception = Assert.Throws<BusinessRuleException>(() => paquete.MoveToEntregado());

        Assert.Contains("estado asignado", exception.Message);
    }

    [Fact]
    public void MoveToEntregado_ChangesEstadoToEntregado_WhenEstadoIsAsignado()
    {
        var paquete = new Paquete
        {
            EstadoId = EstadoPaqueteIds.Asignado
        };

        paquete.MoveToEntregado();

        Assert.Equal(EstadoPaqueteIds.Entregado, paquete.EstadoId);
    }
}