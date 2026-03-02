using MediatR;
using SincoWebApi.Application.Paquetes.Commands;
using SincoWebApi.Domain.Interfaces;

namespace SincoWebApi.Application.Paquetes.Handlers;

public sealed class AssignRepartidorCommandHandler : IRequestHandler<AssignRepartidorCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork;

    public AssignRepartidorCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> Handle(AssignRepartidorCommand request, CancellationToken cancellationToken)
    {
        var paquete = await _unitOfWork.Paquetes.GetByIdAsync(request.PaqueteId, cancellationToken);
        if (paquete is null)
        {
            return false;
        }

        var asignados = await _unitOfWork.PaqueteRepartidores
            .CountAsignadosPorRepartidorAsync(request.Request.RepartidorId, cancellationToken);

        var asignacion = paquete.AssignRepartidor(request.Request.RepartidorId, asignados);

        await _unitOfWork.PaqueteRepartidores.AddAsync(asignacion, cancellationToken);
        _unitOfWork.Paquetes.Update(paquete);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return true;
    }
}