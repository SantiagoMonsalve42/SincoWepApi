using MediatR;
using SincoWebApi.Application.Dtos;
using SincoWebApi.Application.Paquetes.Queries;
using SincoWebApi.Domain.Entities;
using SincoWebApi.Domain.Interfaces;

namespace SincoWebApi.Application.Paquetes.Handlers;

public sealed class GetPaquetesQueryHandler : IRequestHandler<GetPaquetesQuery, IReadOnlyList<PaqueteListItemResponse>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetPaquetesQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IReadOnlyList<PaqueteListItemResponse>> Handle(GetPaquetesQuery request, CancellationToken cancellationToken)
    {
        var paquetes = await _unitOfWork.Paquetes.ListByEstadoAsync(request.EstadoId, cancellationToken);
        return paquetes.Select(MapListItem).ToList();
    }

    private static PaqueteListItemResponse MapListItem(Paquete paquete)
    {
        return new PaqueteListItemResponse
        {
            PaqueteId = paquete.PaqueteId,
            Descripcion = paquete.Descripcion,
            Peso = paquete.Peso,
            CodigoSeguimiento = paquete.CodigoSeguimiento,
            PrioridadId = paquete.PrioridadId,
            Prioridad = paquete.Prioridad?.Descripcion ?? string.Empty,
            EstadoId = paquete.EstadoId,
            Estado = paquete.EstadoPaquete?.Descripcion ?? string.Empty,
            EsPrioridadAlta = paquete.PrioridadId == 1
        };
    }
}