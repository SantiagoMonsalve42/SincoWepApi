using MediatR;
using Microsoft.AspNetCore.Mvc;
using SincoWebApi.Application.Dtos;
using SincoWebApi.Application.EstadosPaquete.Queries;

namespace SincoWepApi.Controllers;

[ApiController]
[Route("api/estados-paquete")]
public sealed class EstadosPaqueteController : ControllerBase
{
    private readonly IMediator _mediator;

    public EstadosPaqueteController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<EstadoPaqueteResponse>>> Get(CancellationToken cancellationToken)
    {
        var estados = await _mediator.Send(new ListEstadosPaqueteQuery(), cancellationToken);
        return Ok(estados);
    }
}