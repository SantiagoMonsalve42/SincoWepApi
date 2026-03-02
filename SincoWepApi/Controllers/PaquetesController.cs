using Microsoft.AspNetCore.Mvc;
using SincoWebApi.Application.Dtos;
using SincoWebApi.Domain.Exceptions;
using SincoWebApi.Application.Services;

namespace SincoWepApi.Controllers;

[ApiController]
[Route("api/paquetes")]
public sealed class PaquetesController : ControllerBase
{
    private readonly IPaqueteService _paqueteService;

    public PaquetesController(IPaqueteService paqueteService)
    {
        _paqueteService = paqueteService;
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<PaqueteListItemResponse>>> Get([FromQuery] int? estadoId, CancellationToken cancellationToken)
    {
        var paquetes = await _paqueteService.ListAsync(estadoId, cancellationToken);
        return Ok(paquetes);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<PaqueteResponse>> GetById(int id, CancellationToken cancellationToken)
    {
        var paquete = await _paqueteService.GetByIdAsync(id, cancellationToken);
        return paquete is null ? NotFound() : Ok(paquete);
    }

    [HttpPost]
    public async Task<ActionResult<PaqueteResponse>> Create([FromBody] PaqueteCreateRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var created = await _paqueteService.CreateAsync(request, cancellationToken);
            return CreatedAtAction(nameof(GetById), new { id = created.PaqueteId }, created);
        }
        catch (BusinessRuleException ex)
        {
            return BadRequest(new { mensaje = ex.Message });
        }
    }

    [HttpPost("{id:int}/asignar")]
    public async Task<IActionResult> Assign(int id, [FromBody] PaqueteAssignRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var assigned = await _paqueteService.AssignRepartidorAsync(id, request, cancellationToken);
            return assigned ? NoContent() : NotFound();
        }
        catch (BusinessRuleException ex)
        {
            return BadRequest(new { mensaje = ex.Message });
        }
    }

    [HttpPost("{id:int}/entregar")]
    public async Task<IActionResult> Deliver(int id, CancellationToken cancellationToken)
    {
        try
        {
            var delivered = await _paqueteService.MoveToEntregadoAsync(id, cancellationToken);
            return delivered ? NoContent() : NotFound();
        }
        catch (BusinessRuleException ex)
        {
            return BadRequest(new { mensaje = ex.Message });
        }
    }
}