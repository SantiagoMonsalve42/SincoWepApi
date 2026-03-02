using System.ComponentModel.DataAnnotations;

namespace SincoWebApi.Application.Dtos;

public sealed class PaqueteAssignRequest
{
    [Range(1, int.MaxValue)]
    public int RepartidorId { get; set; }
}