using MediatR;
using SincoWebApi.Application.Dtos;
using SincoWebApi.Application.EstadosPaquete.Queries;
using SincoWebApi.Domain.Interfaces;

namespace SincoWebApi.Application.EstadosPaquete.Handlers;

public sealed class ListEstadosPaqueteQueryHandler : IRequestHandler<ListEstadosPaqueteQuery, IReadOnlyList<EstadoPaqueteResponse>>
{
    private readonly IUnitOfWork _unitOfWork;

    public ListEstadosPaqueteQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IReadOnlyList<EstadoPaqueteResponse>> Handle(ListEstadosPaqueteQuery request, CancellationToken cancellationToken)
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