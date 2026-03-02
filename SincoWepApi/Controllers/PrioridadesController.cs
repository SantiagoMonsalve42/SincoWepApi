using MediatR;
using Microsoft.AspNetCore.Mvc;
using SincoWebApi.Application.Dtos;
using SincoWebApi.Application.Prioridades.Queries;

namespace SincoWepApi.Controllers;

[ApiController]
[Route("api/prioridades")]
public sealed class PrioridadesController : ControllerBase
{
    private readonly IMediator _mediator;

    public PrioridadesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<PrioridadResponse>>> Get(CancellationToken cancellationToken)
    {
        var prioridades = await _mediator.Send(new ListPrioridadesQuery(), cancellationToken);
        return Ok(prioridades);
    }
}