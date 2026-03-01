namespace SincoWebApi.Domain.Entities;

public sealed class Prioridad
{
    public int PrioridadId { get; set; }
    public string Descripcion { get; set; } = string.Empty;

    public ICollection<Paquete> Paquetes { get; set; } = new List<Paquete>();
}