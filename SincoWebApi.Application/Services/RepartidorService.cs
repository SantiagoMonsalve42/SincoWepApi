using SincoWebApi.Application.Dtos;
using SincoWebApi.Domain.Interfaces;

namespace SincoWebApi.Application.Services;

public sealed class RepartidorService : IRepartidorService
{
    private readonly IUnitOfWork _unitOfWork;

    public RepartidorService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IReadOnlyList<RepartidorResponse>> ListAsync(CancellationToken cancellationToken)
    {
        var repartidores = await _unitOfWork.Repartidores.ListAsync(cancellationToken);

        return repartidores
            .Select(r => new RepartidorResponse
            {
                RepartidorId = r.RepartidorId,
                NombreCompleto = $"{r.Nombre} {r.Apellido}"
            })
            .ToList();
    }
}