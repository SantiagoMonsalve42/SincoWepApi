using MediatR;
using SincoWebApi.Application.Dtos;
using SincoWebApi.Application.Paquetes.Commands;
using SincoWebApi.Domain.Constants;
using SincoWebApi.Domain.Entities;
using SincoWebApi.Domain.Interfaces;

namespace SincoWebApi.Application.Paquetes.Handlers;

public sealed class CreatePaqueteCommandHandler : IRequestHandler<CreatePaqueteCommand, PaqueteResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICodigoSeguimientoGenerator _codigoSeguimientoGenerator;

    public CreatePaqueteCommandHandler(IUnitOfWork unitOfWork, ICodigoSeguimientoGenerator codigoSeguimientoGenerator)
    {
        _unitOfWork = unitOfWork;
        _codigoSeguimientoGenerator = codigoSeguimientoGenerator;
    }

    public async Task<PaqueteResponse> Handle(CreatePaqueteCommand request, CancellationToken cancellationToken)
    {
        var paquete = new Paquete
        {
            Descripcion = request.Request.Descripcion,
            Peso = request.Request.Peso,
            CodigoSeguimiento = _codigoSeguimientoGenerator.Generate(),
            PrioridadId = request.Request.PrioridadId,
            EstadoId = EstadoPaqueteIds.EnBodega
        };

        await _unitOfWork.Paquetes.AddAsync(paquete, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        var saved = await _unitOfWork.Paquetes.GetByIdWithDetailsAsync(paquete.PaqueteId, cancellationToken);
        return Map(saved ?? paquete);
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