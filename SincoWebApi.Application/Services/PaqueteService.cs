using SincoWebApi.Application.Dtos;
using SincoWebApi.Domain.Constants;
using SincoWebApi.Domain.Entities;
using SincoWebApi.Domain.Exceptions;
using SincoWebApi.Domain.Interfaces;

namespace SincoWebApi.Application.Services;

public sealed class PaqueteService : IPaqueteService
{
    private readonly IUnitOfWork _unitOfWork;

    public PaqueteService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<PaqueteResponse> CreateAsync(PaqueteCreateRequest request, CancellationToken cancellationToken)
    {
        var paquete = new Paquete
        {
            Descripcion = request.Descripcion,
            Peso = request.Peso,
            CodigoSeguimiento = GetUniqueId(),
            PrioridadId = request.PrioridadId,
            EstadoId = EstadoPaqueteIds.EnBodega
        };

        await _unitOfWork.Paquetes.AddAsync(paquete, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        var saved = await _unitOfWork.Paquetes.GetByIdWithDetailsAsync(paquete.PaqueteId, cancellationToken);
        return Map(saved ?? paquete);
    }

    public async Task<IReadOnlyList<PaqueteListItemResponse>> ListAsync(int? estadoId, CancellationToken cancellationToken)
    {
        var paquetes = await _unitOfWork.Paquetes.ListByEstadoAsync(estadoId, cancellationToken);
        return paquetes.Select(MapListItem).ToList();
    }

    public async Task<PaqueteResponse?> GetByIdAsync(int paqueteId, CancellationToken cancellationToken)
    {
        var paquete = await _unitOfWork.Paquetes.GetByIdWithDetailsAsync(paqueteId, cancellationToken);
        return paquete is null ? null : Map(paquete);
    }

    public async Task<bool> AssignRepartidorAsync(int paqueteId, PaqueteAssignRequest request, CancellationToken cancellationToken)
    {
        var paquete = await _unitOfWork.Paquetes.GetByIdAsync(paqueteId, cancellationToken);
        if (paquete is null)
        {
            return false;
        }

        var asignados = await _unitOfWork.PaqueteRepartidores
            .CountAsignadosPorRepartidorAsync(request.RepartidorId, cancellationToken);

        var asignacion = paquete.AssignRepartidor(request.RepartidorId, asignados);

        await _unitOfWork.PaqueteRepartidores.AddAsync(asignacion, cancellationToken);
        _unitOfWork.Paquetes.Update(paquete);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return true;
    }

    public async Task<bool> MoveToEntregadoAsync(int paqueteId, CancellationToken cancellationToken)
    {
        var paquete = await _unitOfWork.Paquetes.GetByIdAsync(paqueteId, cancellationToken);
        if (paquete is null)
        {
            return false;
        }

        paquete.MoveToEntregado();
        _unitOfWork.Paquetes.Update(paquete);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return true;
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

    private string GetUniqueId()
    {
        return DateTime.Now.ToString("yyyyMMddHHmmssfff") + Guid.NewGuid().ToString("N").Substring(0, 6).ToUpper();
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