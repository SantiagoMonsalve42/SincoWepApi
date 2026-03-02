using MediatR;
using SincoWebApi.Application.Dtos;
using SincoWebApi.Application.Paquetes.Queries;
using SincoWebApi.Domain.Entities;
using SincoWebApi.Domain.Interfaces;

namespace SincoWebApi.Application.Paquetes.Handlers;

public sealed class GetPaqueteByIdQueryHandler : IRequestHandler<GetPaqueteByIdQuery, PaqueteResponse?>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetPaqueteByIdQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<PaqueteResponse?> Handle(GetPaqueteByIdQuery request, CancellationToken cancellationToken)
    {
        var paquete = await _unitOfWork.Paquetes.GetByIdWithDetailsAsync(request.PaqueteId, cancellationToken);
        return paquete is null ? null : Map(paquete);
    }

    private static PaqueteResponse Map(Paquete paquete)
    {
        return new PaqueteResponse
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