using SincoWebApi.Application.Dtos;

namespace SincoWebApi.Application.Services;

public interface IPaqueteService
{
    Task<PaqueteResponse> CreateAsync(PaqueteCreateRequest request, CancellationToken cancellationToken);
    Task<IReadOnlyList<PaqueteListItemResponse>> ListAsync(int? estadoId, CancellationToken cancellationToken);
    Task<PaqueteResponse?> GetByIdAsync(int paqueteId, CancellationToken cancellationToken);
    Task<bool> AssignRepartidorAsync(int paqueteId, PaqueteAssignRequest request, CancellationToken cancellationToken);
    Task<bool> MoveToEntregadoAsync(int paqueteId, CancellationToken cancellationToken);
}