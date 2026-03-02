namespace SincoWebApi.Application.Dtos;

public sealed class PaqueteResponse
{
    public int PaqueteId { get; set; }
    public string Descripcion { get; set; } = string.Empty;
    public decimal Peso { get; set; }
    public string CodigoSeguimiento { get; set; } = string.Empty;
    public int PrioridadId { get; set; }
    public string Prioridad { get; set; } = string.Empty;
    public int EstadoId { get; set; }
    public string Estado { get; set; } = string.Empty;
    public bool EsPrioridadAlta { get; set; }
}