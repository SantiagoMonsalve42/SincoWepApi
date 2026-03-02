using MediatR;
using SincoWebApi.Application.Dtos;
using SincoWebApi.Application.Prioridades.Queries;
using SincoWebApi.Domain.Interfaces;

namespace SincoWebApi.Application.Prioridades.Handlers;

public sealed class ListPrioridadesQueryHandler : IRequestHandler<ListPrioridadesQuery, IReadOnlyList<PrioridadResponse>>
{
    private readonly IUnitOfWork _unitOfWork;

    public ListPrioridadesQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IReadOnlyList<PrioridadResponse>> Handle(ListPrioridadesQuery request, CancellationToken cancellationToken)
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