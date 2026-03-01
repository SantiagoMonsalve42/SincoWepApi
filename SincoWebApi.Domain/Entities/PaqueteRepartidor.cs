namespace SincoWebApi.Domain.Entities;

public sealed class PaqueteRepartidor
{
    public int PaqueteRepartidorId { get; set; }

    public int RepartidorId { get; set; }
    public Repartidor Repartidor { get; set; } = null!;

    public int PaqueteId { get; set; }
    public Paquete Paquete { get; set; } = null!;
}