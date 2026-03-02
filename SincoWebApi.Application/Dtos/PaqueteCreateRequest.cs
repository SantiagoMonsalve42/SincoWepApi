using System.ComponentModel.DataAnnotations;

namespace SincoWebApi.Application.Dtos;

public sealed class PaqueteCreateRequest
{
    [Required]
    [StringLength(250)]
    public string Descripcion { get; set; } = string.Empty;

    [Range(0.01, 9999)]
    public decimal Peso { get; set; }

    [Range(1, 3)]
    public int PrioridadId { get; set; }
}