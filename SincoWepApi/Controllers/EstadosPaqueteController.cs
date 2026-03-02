using Microsoft.AspNetCore.Mvc;
using SincoWebApi.Application.Dtos;
using SincoWebApi.Application.Services;

namespace SincoWepApi.Controllers;

[ApiController]
[Route("api/estados-paquete")]
public sealed class EstadosPaqueteController : ControllerBase
{
    private readonly IEstadoPaqueteService _estadoPaqueteService;

    public EstadosPaqueteController(IEstadoPaqueteService estadoPaqueteService)
    {
        _estadoPaqueteService = estadoPaqueteService;
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<EstadoPaqueteResponse>>> Get(CancellationToken cancellationToken)
    {
        var estados = await _estadoPaqueteService.ListAsync(cancellationToken);
        return Ok(estados);
    }
}