using MediatR;
using SincoWebApi.Application.Dtos;
using SincoWebApi.Application.Repartidores.Queries;
using SincoWebApi.Domain.Interfaces;

namespace SincoWebApi.Application.Repartidores.Handlers;

public sealed class ListRepartidoresQueryHandler : IRequestHandler<ListRepartidoresQuery, IReadOnlyList<RepartidorResponse>>
{
    private readonly IUnitOfWork _unitOfWork;

    public ListRepartidoresQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IReadOnlyList<RepartidorResponse>> Handle(ListRepartidoresQuery request, CancellationToken cancellationToken)
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