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
}