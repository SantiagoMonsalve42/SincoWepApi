using MediatR;
using Microsoft.AspNetCore.Mvc;
using SincoWebApi.Application.Dtos;
using SincoWebApi.Application.Paquetes.Commands;
using SincoWebApi.Application.Paquetes.Queries;
using SincoWebApi.Domain.Exceptions;

namespace SincoWepApi.Controllers;

[ApiController]
[Route("api/paquetes")]
public sealed class PaquetesController : ControllerBase
{
    private readonly IMediator _mediator;

    public PaquetesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<PaqueteListItemResponse>>> Get([FromQuery] int? estadoId, CancellationToken cancellationToken)
    {
        var paquetes = await _mediator.Send(new GetPaquetesQuery(estadoId), cancellationToken);
        return Ok(paquetes);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<PaqueteResponse>> GetById(int id, CancellationToken cancellationToken)
    {
        var paquete = await _mediator.Send(new GetPaqueteByIdQuery(id), cancellationToken);
        return paquete is null ? NotFound() : Ok(paquete);
    }

    [HttpPost]
    public async Task<ActionResult<PaqueteResponse>> Create([FromBody] PaqueteCreateRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var created = await _mediator.Send(new CreatePaqueteCommand(request), cancellationToken);
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
            var assigned = await _mediator.Send(new AssignRepartidorCommand(id, request), cancellationToken);
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
            var delivered = await _mediator.Send(new DeliverPaqueteCommand(id), cancellationToken);
            return delivered ? NoContent() : NotFound();
        }
        catch (BusinessRuleException ex)
        {
            return BadRequest(new { mensaje = ex.Message });
        }
    }
}