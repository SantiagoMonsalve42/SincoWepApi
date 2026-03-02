using SincoWebApi.Application.Dtos;

namespace SincoWebApi.Application.Services;

public interface IEstadoPaqueteService
{
    Task<IReadOnlyList<EstadoPaqueteResponse>> ListAsync(CancellationToken cancellationToken);
}