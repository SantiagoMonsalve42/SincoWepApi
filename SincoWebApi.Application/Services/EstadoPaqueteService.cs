using SincoWebApi.Application.Dtos;
using SincoWebApi.Domain.Interfaces;

namespace SincoWebApi.Application.Services;

public sealed class EstadoPaqueteService : IEstadoPaqueteService
{
    private readonly IUnitOfWork _unitOfWork;

    public EstadoPaqueteService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IReadOnlyList<EstadoPaqueteResponse>> ListAsync(CancellationToken cancellationToken)
    {
        var estados = await _unitOfWork.EstadosPaquete.ListAsync(cancellationToken);

        return estados
            .Select(e => new EstadoPaqueteResponse
            {
                EstadoId = e.EstadoId,
                Descripcion = e.Descripcion
            })
            .ToList();
    }
}