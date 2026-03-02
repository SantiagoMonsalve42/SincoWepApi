using MediatR;
using Microsoft.AspNetCore.Mvc;
using SincoWebApi.Application.Dtos;
using SincoWebApi.Application.Repartidores.Queries;

namespace SincoWepApi.Controllers;

[ApiController]
[Route("api/repartidores")]
public sealed class RepartidoresController : ControllerBase
{
    private readonly IMediator _mediator;

    public RepartidoresController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<RepartidorResponse>>> Get(CancellationToken cancellationToken)
    {
        var repartidores = await _mediator.Send(new ListRepartidoresQuery(), cancellationToken);
        return Ok(repartidores);
    }
}