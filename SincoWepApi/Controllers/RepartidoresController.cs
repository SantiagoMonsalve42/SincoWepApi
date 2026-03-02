using Microsoft.AspNetCore.Mvc;
using SincoWebApi.Application.Dtos;
using SincoWebApi.Application.Services;

namespace SincoWepApi.Controllers;

[ApiController]
[Route("api/repartidores")]
public sealed class RepartidoresController : ControllerBase
{
    private readonly IRepartidorService _repartidorService;

    public RepartidoresController(IRepartidorService repartidorService)
    {
        _repartidorService = repartidorService;
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<RepartidorResponse>>> Get(CancellationToken cancellationToken)
    {
        var repartidores = await _repartidorService.ListAsync(cancellationToken);
        return Ok(repartidores);
    }
}