using SincoWebApi.Application.Dtos;

namespace SincoWebApi.Application.Services;

public interface IPrioridadService
{
    Task<IReadOnlyList<PrioridadResponse>> ListAsync(CancellationToken cancellationToken);
}