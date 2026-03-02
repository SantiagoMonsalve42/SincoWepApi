using MediatR;
using SincoWebApi.Application.Paquetes.Commands;
using SincoWebApi.Domain.Interfaces;

namespace SincoWebApi.Application.Paquetes.Handlers;

public sealed class DeliverPaqueteCommandHandler : IRequestHandler<DeliverPaqueteCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeliverPaqueteCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> Handle(DeliverPaqueteCommand request, CancellationToken cancellationToken)
    {
        var paquete = await _unitOfWork.Paquetes.GetByIdAsync(request.PaqueteId, cancellationToken);
        if (paquete is null)
        {
            return false;
        }

        paquete.MoveToEntregado();
        _unitOfWork.Paquetes.Update(paquete);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return true;
    }
}