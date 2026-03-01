namespace SincoWebApi.Domain.Entities;

public sealed class Repartidor
{
    public int RepartidorId { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string Apellido { get; set; } = string.Empty;

    public ICollection<PaqueteRepartidor> PaqueteRepartidores { get; set; } = new List<PaqueteRepartidor>();
}