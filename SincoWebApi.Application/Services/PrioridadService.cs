using SincoWebApi.Application.Dtos;
using SincoWebApi.Domain.Interfaces;

namespace SincoWebApi.Application.Services;

public sealed class PrioridadService : IPrioridadService
{
    private readonly IUnitOfWork _unitOfWork;

    public PrioridadService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IReadOnlyList<PrioridadResponse>> ListAsync(CancellationToken cancellationToken)
    {
        var prioridades = await _unitOfWork.Prioridades.ListAsync(cancellationToken);

        return prioridades
            .Select(p => new PrioridadResponse
            {
                PrioridadId = p.PrioridadId,
                Descripcion = p.Descripcion
            })
            .ToList();
    }
}