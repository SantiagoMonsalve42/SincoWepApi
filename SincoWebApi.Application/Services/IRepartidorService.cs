using SincoWebApi.Application.Dtos;

namespace SincoWebApi.Application.Services;

public interface IRepartidorService
{
    Task<IReadOnlyList<RepartidorResponse>> ListAsync(CancellationToken cancellationToken);
}