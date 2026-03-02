using SincoWebApi.Domain.Constants;
using SincoWebApi.Domain.Exceptions;

namespace SincoWebApi.Domain.Entities;

public sealed class Paquete
{
    public int PaqueteId { get; set; }
    public string Descripcion { get; set; } = string.Empty;
    public decimal Peso { get; set; }
    public string CodigoSeguimiento { get; set; } = string.Empty;

    public int PrioridadId { get; set; }
    public Prioridad Prioridad { get; set; } = null!;

    public int EstadoId { get; set; }
    public EstadoPaquete EstadoPaquete { get; set; } = null!;

    public ICollection<PaqueteRepartidor> PaqueteRepartidores { get; set; } = new List<PaqueteRepartidor>();

    public PaqueteRepartidor AssignRepartidor(int repartidorId, int asignados)
    {
        if (EstadoId != EstadoPaqueteIds.EnBodega)
        {
            throw new BusinessRuleException("El paquete no está en bodega.");
        }

        if (asignados >= 3)
        {
            throw new BusinessRuleException("El repartidor no puede aceptar más paquetes.");
        }

        var asignacion = new PaqueteRepartidor
        {
            PaqueteId = PaqueteId,
            RepartidorId = repartidorId,
            Paquete = this
        };

        PaqueteRepartidores.Add(asignacion);
        EstadoId = EstadoPaqueteIds.Asignado;

        return asignacion;
    }

    public void MoveToEntregado()
    {
        if (EstadoId != EstadoPaqueteIds.Asignado)
        {
            throw new BusinessRuleException("El paquete no está en estado asignado.");
        }

        EstadoId = EstadoPaqueteIds.Entregado;
    }
}