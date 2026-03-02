using Microsoft.AspNetCore.Mvc;
using SincoWebApi.Application.Dtos;
using SincoWebApi.Application.Services;

namespace SincoWepApi.Controllers;

[ApiController]
[Route("api/prioridades")]
public sealed class PrioridadesController : ControllerBase
{
    private readonly IPrioridadService _prioridadService;

    public PrioridadesController(IPrioridadService prioridadService)
    {
        _prioridadService = prioridadService;
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<PrioridadResponse>>> Get(CancellationToken cancellationToken)
    {
        var prioridades = await _prioridadService.ListAsync(cancellationToken);
        return Ok(prioridades);
    }
}