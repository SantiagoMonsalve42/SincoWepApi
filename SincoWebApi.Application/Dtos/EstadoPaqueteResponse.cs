namespace SincoWebApi.Application.Dtos;

public sealed class EstadoPaqueteResponse
{
    public int EstadoId { get; set; }
    public string Descripcion { get; set; } = string.Empty;
}